<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_OrderInfo_Integral.aspx.cs" Inherits="Admin_TJ_OrderInfo_Integral" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem"><span>起始时间</span></div>
                <div class="topitem">
                    <input id="txt_start" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div>
                <div class="topitem">
                    <input id="txt_end" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div> 
                <div class="topitem">
                    <asp:DropDownList runat="server" ID="ddl_UserType">
                        <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">消费者</asp:ListItem>
                        <asp:ListItem Value="3">终端店</asp:ListItem> 
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <asp:DropDownList runat="server" ID="ddl_jiangpin" AppendDataBoundItems="True" DataTextField="AwardThing" DataValueField="AWID">
                        <asp:ListItem Value="0" Selected="True">全部奖品</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="CustomerName">客户</asp:ListItem>
                        <asp:ListItem Value="CustomerPhone">联系人电话</asp:ListItem> 
                        <asp:ListItem Value="WuLiuCompName">物流公司</asp:ListItem>
                        <asp:ListItem Value="WuLiuDanHao">物流单号</asp:ListItem>
                        <asp:ListItem Value="Address">地址</asp:ListItem> 
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                <div class="topitem">
                    <asp:Button runat="server" Text="导出EXCEL" ID="btn_excel_out" CssClass="btn btn-info btnyd" OnClick="btn_excel_out_Click"/>
                </div>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="OrderID,Show,OrderStatusID"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
               <%--<asp:TemplateField HeaderText="公司">
                            <ItemTemplate>
                                <asp:Label ID="LabelCompID" runat="server" Text='<%# Bind("CompID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="编号">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderNumber" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖品">
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsID" runat="server" Text='<%# BtjAward.GetList(int.Parse(Eval("GoodsID").ToString())).AwardThing  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderNum" runat="server" Text='<%# Bind("OrderNum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                 <%--<asp:TemplateField HeaderText="中奖人">
                            <ItemTemplate>
                                <asp:Label ID="LabelUserID" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="提交时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderDate" runat="server" Text='<%# Bind("OrderDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="联系人">
                            <ItemTemplate>
                                <asp:Label ID="LabelCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="联系电话">
                            <ItemTemplate>
                                <asp:Label ID="LabelCustomerPhone" runat="server" Text='<%# Bind("CustomerPhone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="详细地址">
                            <ItemTemplate>
                                <asp:Label ID="LabelAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="处理人">
                            <ItemTemplate>
                                <asp:Label ID="LabelDeliveryUserID" runat="server" Text='<%# Bind("DeliveryUserNM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物流公司">
                            <ItemTemplate>
                                <asp:Label ID="LabelWuLiuCompName" runat="server" Text='<%# Bind("WuLiuCompName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="物流单号">
                            <ItemTemplate>
                                <asp:Label ID="LabelWuLiuDanHao" runat="server" Text='<%# Bind("WuLiuDanHao") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货日期">
                            <ItemTemplate>
                                <asp:Label ID="LabelDeliveryComfirmDate" runat="server" Text='<%# Bind("DeliveryComfirmDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="费用">
                            <ItemTemplate>
                                ￥<asp:Label ID="LabelYunFei" runat="server" Text='<%# Bind("YunFei") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="LabelIsShouHuo" runat="server" Text='<%# (Convert.ToInt32(Eval("OrderStatusID")).Equals(2)?"已处理":"未处理")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
            </div>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    </form>
</body>
</html>
