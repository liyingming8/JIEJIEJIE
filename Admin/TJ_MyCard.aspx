<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_MyCard.aspx.cs" Inherits="Admin_TJ_MyCard" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <p>昵称：</p>
                </div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入昵称" class="inputsearch" />
                </div>

                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <div class="topdiv">
                    <div class="topitem">
                        <asp:Button runat="server" ID="btn_createexcel" Style="text-decoration: underline; cursor: pointer;" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click" />
                    </div>
                </div>
            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto">
                        <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False"
                            CssClass="GridViewStyle" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="userid">
                            <Columns>
                                <asp:TemplateField HeaderText="昵称">
                                    <ItemTemplate>
                                        <asp:Label ID="NickName" runat="server" Text='<%# Bind("NickName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--         
                                <asp:TemplateField HeaderText="奖品">
                                    <ItemTemplate>
                                        <asp:Label ID="prizevl" runat="server" Text='<%# FindAwardThingFromAWID(Eval("awid").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                --%>
                                <asp:TemplateField HeaderText="卡片数量">
                                    <ItemTemplate>
                                        <asp:Label ID="total" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="卡片详情">
                                    <ItemTemplate>
                                        <center>
                                            <asp:HyperLink ID="hyplink" CssClass="btn btn-default btnydinline" runat="server">详情</asp:HyperLink></center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--                          
                                <asp:TemplateField HeaderText="类型">
                                    <ItemTemplate>
                                        <asp:Label ID="gettm" runat="server" Text='<%# FindAwardTypeFromAwTypeId(Eval("awtypeid").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="备注">
                                    <ItemTemplate>
                                        <asp:Label ID="remark" runat="server" Text='<%# Bind("remark") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                --%>
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

