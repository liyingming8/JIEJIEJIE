﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerinfo.aspx.cs" Inherits="CRM_tj_crm_customerinfo" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="topdiv">
            <div class="topitem"><span></span></div>
            <div class="topitem">
                <asp:DropDownList ID="DDLField" runat="server">
                    <asp:ListItem Value="customername">客户姓名</asp:ListItem>
                    <asp:ListItem Value="idcardnumber">证件号码</asp:ListItem>
                    <asp:ListItem Value="sexinfo">性别</asp:ListItem>
                    <asp:ListItem Value="phonenumber">手机</asp:ListItem>
                    <asp:ListItem Value="telnumber">电话</asp:ListItem>
                    <asp:ListItem Value="faxnumber">传真</asp:ListItem>
                    <asp:ListItem Value="addressinfo">地址</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="topitem">
                <asp:DropDownList ID="StatusDropDownList" runat="server">
                    <asp:ListItem Value="All">全部</asp:ListItem>
                    <asp:ListItem Value="Checked">已审核</asp:ListItem>
                    <asp:ListItem Value="NonChecked">未审核</asp:ListItem>                  
                </asp:DropDownList>
            </div>

            <div class="topitem">
                <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" placeholder="请输入查找内容" /></div>
            <div class="topitem">
                <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
        </div>
        <div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="../Admin/image/edit.png" alt="Edit" height="25" id="editimg" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="父级">
                            <ItemTemplate>
                                <asp:Label ID="Labelparentid" runat="server" Text='<%# Eval("parentid").ToString().Equals("0")?"——":ReturnCustomerName(Eval("parentid").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="客户姓名">
                            <ItemTemplate>
                                <asp:Label ID="Labelcustomername" runat="server" Text='<%# Bind("customername") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="级别">
                            <ItemTemplate>
                                <asp:Label ID="Labelgradename" runat="server" Text='<%# ReturnCustomerGradeName(Eval("gradeid").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="Status" runat="server" Text='<%# ReturnIsPermit(Eval("ispermit").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="微信号">
                            <ItemTemplate>
                                <asp:Label ID="Labelusername" runat="server" Text='<%# Bind("wxusernumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="证件号码">
                            <ItemTemplate>
                                <asp:Label ID="Labelidcardnumber" runat="server" Text='<%# Bind("idcardnumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="性别">
                            <ItemTemplate>
                                <asp:Label ID="Labelsexinfo" runat="server" Text='<%# Bind("sexinfo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="手机">
                            <ItemTemplate>
                                <asp:Label ID="Labelphonenumber" runat="server" Text='<%# Bind("phonenumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="授权号">
                            <ItemTemplate>
                                <asp:Label ID="Labelfaxnumber" runat="server" Text='<%# Bind("authorcode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="城市/地区">
                            <ItemTemplate>
                                <asp:Label ID="Labelcityid" runat="server" Text='<%# Bind("cityarea")  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="详细地址">
                            <ItemTemplate>
                                <asp:Label ID="Labeladdressinfo" runat="server" Text='<%# Bind("addressinfo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="证书">
                            <ItemTemplate>
                                <asp:Button ID="Button_ProZhengShu" CommandArgument='<%# Bind("id") %> ' CommandName="prozs" runat="server" Text="生成" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle  HorizontalAlign="Center" Width="50px" /></asp:CommandField>--%>
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
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="10" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth="" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" ShowBoxThreshold="20" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到"></webdiyer:AspNetPager>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>