<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AwardInfo.aspx.cs" Inherits="Admin_TJ_AwardInfo" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_AwardInfoAddEdit.aspx?cmd=add', 720, 660, '积分奖品管理')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="AwardThing">奖品</asp:ListItem> 
                        <asp:ListItem Value="Contents">内容</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div> 
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False" DataKeyNames="AWID,kucunshuliang,AwardThing,goods_from_compid" OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>  
                    <asp:TemplateField HeaderText="类别">
                        <ItemTemplate>
                            <asp:Label ID="LabelAwardType" runat="server" Text='<%# BtjAwardType.GetList(int.Parse(Eval("AwardType").ToString())).awardtype %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="奖品图片">
                        <ItemTemplate>
                            <asp:Image ID="imageSmallURLString" runat="server" Width="80px" ImageUrl='<%# (string.IsNullOrEmpty(Eval("ImageURLString").ToString())?"~/Admin/Images/NoPic.gif":Eval("ImageURLString").ToString().Replace("http://","https://")) %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖品">
                        <ItemTemplate>
                            <asp:Label ID="LabelAwardThing"  runat="server" ToolTip='<%# Eval("AwardThing").ToString() %>' Text='<%# (Eval("AwardThing").ToString().Length>10?Eval("AwardThing").ToString().Substring(0,10):Eval("AwardThing").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所需积分">
                        <ItemTemplate>
                            <asp:Label ID="LabelIntegralValue" runat="server" Text='<%# Bind("IntegralValue") %>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="价格">
                        <ItemTemplate>
                            ￥<asp:Label ID="Labelprice" runat="server" Text='<%# Bind("CashValue") %>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖品介绍">
                        <ItemTemplate>
                            <asp:Label ID="LabelContents" runat="server" ToolTip='<%#Eval("Contents").ToString() %>' Text='<%# (Eval("Contents").ToString().Length>15?Eval("Contents").ToString().Substring(0,15):Eval("Contents").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="积分兑换">
                        <ItemTemplate><asp:CheckBox runat="server" Enabled="False" Checked='<%# Convert.ToBoolean(Eval("ExchangeIntegral")) %>' ID="ckb_ExchangeIntegral"/></ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="兑换现金">
                        <ItemTemplate><asp:CheckBox runat="server"  Enabled="False" Checked='<%# Convert.ToBoolean(Eval("ExchangeIntegral")) %>'  ID="ckb_ExchangeCash"/></ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发布时间">
                        <ItemTemplate>
                            <asp:Label ID="LabelPublishDate" runat="server" Text='<%# Bind("PublishDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="有效">
                        <ItemTemplate><asp:CheckBox runat="server" Enabled="False"  Checked='<%# Convert.ToBoolean(Eval("IsActive")) %>'  ID="ckb_isactive"/></ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库存">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplinkkucun" runat="server"><%# Eval("kucunshuliang").ToString() %>(件)</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖励对象">
                        <ItemTemplate>
                            <asp:Label ID="lbfacetu" runat="server"><%# FaceToString(Eval("faceto").ToString()) %></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="系列图">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ID="hlink_img_view">查看</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">  
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- 
                    <asp:CommandField ShowDeleteButton="True" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>--%>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="10"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

        </div>
    </form>
    <script type="text/javascript" src="../include/js/jquery-2.1.1.min.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
