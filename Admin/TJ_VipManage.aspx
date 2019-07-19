<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_VipManage.aspx.cs" Inherits="Admin_TJ_VipManage" %>

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
                 <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_VipManageAddEdit.aspx?cmd=add', 600, 400, '会员管理')" /> 
                </div>
            <div class="topdiv">
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <%--<asp:ListItem Value="VipID">VipID</asp:ListItem>--%>
                        <asp:ListItem Value="MPhone">手机号码
                        </asp:ListItem>
                        <%--<asp:ListItem Value="Email">Email</asp:ListItem>
<asp:ListItem Value="ZhuCTime">ZhuCTime</asp:ListItem>
<asp:ListItem Value="Address">Address</asp:ListItem>
<asp:ListItem Value="DelFlag">DelFlag</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
                <div class="topitem">
                    <asp:Button ID="BtnSearch1" runat="server" Text="导出Excel数据" CssClass="btn btn-warning btnyd" OnClick="BtnSearch2_Click" />
                </div>
           
               
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="VipID"
                  OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>

                    <%--                <asp:TemplateField HeaderText="会员名称">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtVipName" runat="server" Text='<%# Bind("VipName") %>' MaxLength="50"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelVipName" runat="server" Text='<%# Bind("VipName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="手机号码">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMPhone" runat="server" Text='<%# Bind("MPhone") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelMPhone" runat="server" Text='<%# Bind("MPhone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="邮箱">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' MaxLength="50"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="注册时间">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtZhuCTime" runat="server" Text='<%# Bind("ZhuCTime") %>' MaxLength="16"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelZhuCTime" runat="server" Text='<%# Bind("ZhuCTime") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="可用积分">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtJiFen" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("JiFen") %>' MaxLength="4"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelJiFen" runat="server" Text='<%# Bind("JiFen") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="所在地区">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address") %>' MaxLength="50"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelAddress" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <%-- <asp:TemplateField HeaderText="PlaceID"   Visible="false" >
                    <EditItemTemplate>
                    <asp:TextBox ID="txtPlaceID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("PlaceID") %>' MaxLength="4"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelPlaceID" runat="server" Text='<%# Bind("PlaceID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="CompID" Visible="false">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompID" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("CompID") %>' MaxLength="4"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelCompID" runat="server" Text='<%# Bind("CompID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle  HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
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
