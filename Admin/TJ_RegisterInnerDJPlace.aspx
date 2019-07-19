<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterInnerDJPlace.aspx.cs" Inherits="Admin_TJ_RegisterInnerDJPlace" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_RegisterInnerDJPlaceAddEdit.aspx?cmd=add', 600, 500, '兑奖点管理')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Text="兑奖点" Value="CompName"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
            </div> 
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"
                AutoGenerateColumns="False" DataKeyNames="CompID" 
                OnRowDeleting="GridView1_RowDeleting" 
                 OnRowDataBound="GridView1_RowDataBound"
                CssClass="GridViewStyle" PageSize="18">
                <Columns>
                    <asp:TemplateField HeaderText="兑奖点">
                        <ItemTemplate>
                            <asp:Label ID="LabelCompName" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="联系人">
                        <ItemTemplate>
                            <asp:Label ID="LabelLegalPerson" runat="server"
                                Text='<%# Bind("LegalPerson") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="城市">
                        <ItemTemplate>
                            <asp:Label ID="LabelCTID" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("CTID").ToString(), true, false) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="地址">
                        <ItemTemplate>
                            <asp:Label ID="LabelAddress" runat="server" Text='<%# ((Eval("Address").ToString().Length > 15) ? Eval("Address").ToString().Substring(0, 14) : Eval("Address").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="联系电话">
                        <ItemTemplate>
                            <asp:Label ID="LabelTelNumber" runat="server" Text='<%# Bind("TelNumber") %>'></asp:Label>
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
                    <%--<asp:HyperLinkField DataNavigateUrlFields="CompID" Text="管理" DataNavigateUrlFormatString="TJ_CompanyImageInfo.aspx?CPID={0}" HeaderText="图片" />--%>
                    <%--<asp:HyperLinkField DataNavigateUrlFields="CompID" Text="生成" DataNavigateUrlFormatString="QrProduce.aspx?ID={0}&tp=smck" Target="_blank" HeaderText="盟友验证QR" />--%>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                
                <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

        </div>
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
