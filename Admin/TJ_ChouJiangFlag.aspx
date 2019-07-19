<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ChouJiangFlag.aspx.cs" Inherits="Admin_TJ_ChouJiangFlag" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <script src="../include/js/Wdate/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">

            <div class="topdiv">
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="UserName">抽奖人</asp:ListItem>
                        <asp:ListItem Value="JXName">奖品名称</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
                <div class="topitem">
                    <span>起始时间</span>
                </div>
                <div class="topitem">
                    <input id="input_start_date" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div>
                <div class="topitem">
                    <span>至</span>
                </div>
                <div class="topitem">
                    <input id="input_end_date" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <div class="topitem">
                    <asp:Button runat="server" ID="btn_createexcel" Style="text-decoration: underline; cursor: pointer;" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click" />
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto">
                        <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False"
                            CssClass="GridViewStyle" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="">
                            <Columns>
                                <asp:TemplateField HeaderText="抽奖人昵称">
                                    <ItemTemplate>
                                        <asp:Label ID="UserID" runat="server" Text='<%# FindUserNameFromUserId(Eval("UserID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>             
                                <asp:TemplateField HeaderText="奖品名称">
                                    <ItemTemplate>
                                        <asp:Label ID="JXName" runat="server" Text='<%# Bind("JXName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                       
                                <asp:TemplateField HeaderText="抽奖时间">
                                    <ItemTemplate>
                                        <asp:Label ID="LQtime" runat="server" Text='<%# Bind("LQtime","{0:yyyy-MM-dd hh:mm:ss}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:Label ID="Rmarks" runat="server" Text='<%# Bind("Rmarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="10" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div>
    </form>
</body>
</html>
