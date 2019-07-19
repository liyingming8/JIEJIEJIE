﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_GoodsInfoSimple.aspx.cs" Inherits="Admin_TJ_GoodsInfoSimple" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_GoodsInfoAddEditSimple.aspx?cmd=add', 560, 500, '产品上架管理')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="GoodsName">产品名称</asp:ListItem>
                        <asp:ListItem Value="Descriptions">描述</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
            <%--    <div  style="display:none">
                    <div class="topitem">所属公司</div>
                    <div class="topitem">
                        <input id="incompany" runat="server" readonly="readonly" class="inputsearch" style="width: 250px" /></div>
                    <div class="topitem">
                        <asp:CheckBox ID="checkall" Visible="<%# IsSuperAdmin() %>" runat="server" Text="全部" />
                    </div>
                </div>--%>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
            </div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView1" EnableViewState="False" runat="server" PageSize="18" Width="100%" AutoGenerateColumns="False" DataKeyNames="GoodsID"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>  
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品名称"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsName" runat="server" Text='<%# Bind("GoodsName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="参考价"> 
                            <ItemTemplate>
                                ￥<asp:Label ID="LabelPrice" runat="server" Text='<%# Bind("Price") %>'></asp:Label>/<asp:Label ID="labSaleUnit" Text='<%# Bind("SaleUnitID") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="上架时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelBeginSaleDate" runat="server" Text='<%# Bind("BeginSaleDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下架时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelEndSaleDate" runat="server" Text='<%# Bind("EndSaleDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="备注"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:CommandField ShowDeleteButton="True" Visible="<%#IsSuperAdmin()%>" ButtonType="Image" DeleteImageUrl="~/Admin/image/delete.png">
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:CommandField>--%>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                    <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

        </div>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
        <asp:HiddenField ID="hf_compid" runat="server" Visible="False" />
    </form>
</body>
</html>
