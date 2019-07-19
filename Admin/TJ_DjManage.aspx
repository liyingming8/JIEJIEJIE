<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DjManage.aspx.cs" Inherits="Admin_TJ_DjManage" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <%--<asp:ListItem Value="ZjID">ZjID</asp:ListItem>
<asp:ListItem Value="ZjNumber">ZjNumber</asp:ListItem>
<asp:ListItem Value="ZjTime">ZjTime</asp:ListItem>
<asp:ListItem Value="JxName">JxName</asp:ListItem>--%>
                        <asp:ListItem Value="ZjPhone">中奖手机号</asp:ListItem>
                        <%--<asp:ListItem Value="CpXS">CpXS</asp:ListItem>
<asp:ListItem Value="CpCX">CpCX</asp:ListItem>--%>
                        <asp:ListItem Value="DjdName">兑奖点名称</asp:ListItem>
                        <%--<asp:ListItem Value="DhTime">DhTime</asp:ListItem>
<asp:ListItem Value="DjFlag">DjFlag</asp:ListItem>
<asp:ListItem Value="LjTime">LjTime</asp:ListItem>
<asp:ListItem Value="ZjyzCode">ZjyzCode</asp:ListItem>
<asp:ListItem Value="DelFlag">DelFlag</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch1" runat="server" Text="导出Excel数据" CssClass="btn btn-warning btnyd" OnClick="BtnSearch2_Click " />
                </div>
               
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="ZjID"
                  OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <%--<asp:HyperLinkField DataNavigateUrlFields="ZjID" DataNavigateUrlFormatString="TJ_DjManageAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />--%>

                    <asp:TemplateField HeaderText="中奖标签号码">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtZjNumber" runat="server" Text='<%# Bind("ZjNumber") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelZjNumber" runat="server" Text='<%# Bind("ZjNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中奖时间">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtZjTime" runat="server" Text='<%# Bind("ZjTime") %>' MaxLength="16"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelZjTime" runat="server" Text='<%# Bind("ZjTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖项名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtJxName" runat="server" Text='<%# Bind("JxName") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelJxName" runat="server" Text='<%# Bind("JxName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中奖手机号码">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtZjPhone" runat="server" Text='<%# Bind("ZjPhone") %>' MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelZjPhone" runat="server" Text='<%# Bind("ZjPhone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="销售地区">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCpXS" runat="server" Text='<%# Bind("CpXS") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelCpXS" runat="server" Text='<%# Bind("CpXS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="查询地区">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCpCX" runat="server" Text='<%# Bind("CpCX") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelCpCX" runat="server" Text='<%# Bind("CpCX") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="兑奖点">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDjdName" runat="server" Text='<%# Bind("DjdName") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelDjdName" runat="server" Text='<%# Bind("DjdName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="兑换时间">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDhTime" runat="server" Text='<%# Bind("DhTime") %>' MaxLength="16"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelDhTime" runat="server" Text='<%# Bind("DhTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="是否领奖">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDjFlag" runat="server" Text='<%# Bind("DjFlag") %>' MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelDjFlag" runat="server" Text='<%# Bind("DjFlag") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="领奖时间">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLjTime" runat="server" Text='<%# Bind("LjTime") %>' MaxLength="16"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelLjTime" runat="server" Text='<%# Bind("LjTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="中奖验证码">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtZjyzCode" runat="server" Text='<%# Bind("ZjyzCode") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelZjyzCode" runat="server" Text='<%# Bind("ZjyzCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DelFlag" Visible="false">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDelFlag" runat="server" MaxLength="2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelDelFlag" runat="server" Text='<%# Bind("DelFlag") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CommandField ShowEditButton="True" />
<asp:CommandField ShowDeleteButton="True" />--%>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
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
