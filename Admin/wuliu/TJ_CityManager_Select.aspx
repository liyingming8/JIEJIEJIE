﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CityManager_Select.aspx.cs" Inherits="Admin_wuliu_TJ_CityManager_Select" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
     <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <asp:DropDownList DataTextField="department" DataValueField="id" runat="server" ID="ddl_departid">
                        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="LoginName">用户名</asp:ListItem>
                        <asp:ListItem Value="NickName">昵称</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto">
                        <asp:GridView ID="GridView1" EnableViewState="False" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="UserID,CompID,RID,LoginName" 
                            CssClass="GridViewStyle" PageSize="18" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="所属战区"> 
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRID" runat="server" Text='<%# BtjDepart.GetList(int.Parse(Eval("departid").ToString())).department %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="用户名">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRegisterDate" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView>
                    </div>
                    <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth="" NextPageText="下一页" PrevPageText="上一页" NumericButtonCount="5" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页"></webdiyer:AspNetPager>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
        <input id="rid" runat="server" type="hidden" />
        <input id="fr" runat="server" type="hidden" />
        <input id="uid" runat="server" type="hidden"/>
    </form>
</body>
</html>