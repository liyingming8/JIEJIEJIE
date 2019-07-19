﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_Infor.aspx.cs" Inherits="Admin_TB_Products_Infor" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TB_Products_InforAddEdit.aspx?cmd=add',700, 550, '产品信息编辑')" /></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="Product_Code">产品编码</asp:ListItem>
                        <asp:ListItem Value="Products_Name">产品名称</asp:ListItem>
                        <asp:ListItem Value="Products_Summary">简介</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False" DataKeyNames="Infor_ID"
                      OnRowDeleting="GridView1_RowDeleting"  OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="产品类别">
                            <ItemTemplate>
                                <asp:Label ID="LabelTypeId" runat="server" Text='<%# (Eval("TypeId").ToString().Trim()=="" ? "" : ptype.GetList(Convert.ToInt32(Eval("TypeId"))).TypeName) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="规格">
                            <ItemTemplate>
                                <asp:Label ID="LabelPSID" runat="server" Text='<%# (Eval("PSID").ToString().Trim()=="" ? "" : pstandards.GetList(Convert.ToInt32(Eval("PSID").ToString())).StandarsDes) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品编码">
                            <ItemTemplate>
                                <asp:Label ID="LabelProduct_Code" runat="server" Text='<%# Bind("Product_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProducts_Name" runat="server" Text='<%# Bind("Products_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelProducts_Name" runat="server" Text='<%# Bind("Products_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="价格">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProducts_Price" runat="server" Text='<%# Bind("Products_Price") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelProducts_Price" runat="server" Text='<%# Bind("Products_Price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="香型">
                            <ItemTemplate>
                                <asp:Label ID="LabelProductXiangXing" runat="server" Text='<%# (Eval("ProductXiangXing").ToString().Trim()==""  ? "" : pxiangxing.GetList(Convert.ToInt32(Eval("ProductXiangXing").ToString())).XiangXing) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="酒精度">
                            <ItemTemplate>
                                <asp:Label ID="LabelProductJiuJingDu" runat="server" Text='<%# (Eval("ProductJiuJingDu").ToString().Trim()=="" ? "" : pjiujingdu.GetList(Convert.ToInt32(Eval("ProductJiuJingDu").ToString())).JiuJingDuShu) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="净含量">
                            <ItemTemplate>
                                <asp:Label ID="LabelProductJingHanLiang" runat="server" Text='<%# (Eval("ProductJingHanLiang").ToString().Trim()=="" ? "" : pjinghanliang.GetList(Convert.ToInt32(Eval("ProductJingHanLiang").ToString())).JingHanLiang) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品简介">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProducts_Summary" runat="server" Text='<%# Bind("Products_Summary") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelProducts_Summary" runat="server" Text='<%# Bind("Products_Summary") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="自主品牌">
                            <ItemTemplate>
                                <asp:CheckBox ID="checkboxisown" runat="server" Enabled="false" Checked='<%# Bind("IsOwn") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品图像">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="原料">
                            <ItemTemplate>
                                <asp:Label ID="Labelyuanliao" runat="server" Text='<%# (Eval("MTID").ToString().Trim()=="" ? "" : byuanliao.GetList(Convert.ToInt32(Eval("MTID").ToString())).MTname) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标准">
                            <ItemTemplate>
                                <%--   <asp:Label ID="Labelbiaozhun" runat="server" Text='<%# (Eval("BZID").Equals(null) ? "" : bbiaozhun.GetList(Convert.ToInt32(Eval("BZID").ToString())).BiaoZhunname) %>'></asp:Label>--%>
                                <asp:Label ID="Labelbiaozhun" runat="server" Text='<%# (Eval("BZID").ToString().Trim()=="" ? "" : bbiaozhun.GetList(Convert.ToInt32(Eval("BZID").ToString())).BiaoZhunname) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:CommandField ShowEditButton="True" />--%>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div>
    </form>
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
