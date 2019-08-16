<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegistercommonswmShouQuan.aspx.cs" Inherits="Admin_commonswm_TJ_RegistercommonswmShouQuan" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Text="名称" Value="CompName"></asp:ListItem>
                    </asp:DropDownList>
                </div>              
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"
                AutoGenerateColumns="False" DataKeyNames="CompID,MasterID" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" PageSize="18">
                <Columns>
                    <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="../image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate>
                            <asp:Label ID="LabelCompName" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="激活状态">
                        <ItemTemplate>
                            <asp:Label ID="LabelJiHuoID" runat="server"
                                Text='<%# JiJuoInfo(Eval("CompAutherID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                   
                    <asp:TemplateField HeaderText="授权状态">
                        <ItemTemplate>
                            <asp:Label ID="LabelCompAutherID" runat="server"
                                Text='<%# ShouQuanInfo(Eval("MasterID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="地址">
                        <ItemTemplate>
                            <asp:Label ID="LabelAddress" runat="server" Text='<%# ((Eval("Address").ToString().Length > 15) ? Eval("Address").ToString().Substring(0, 14) : Eval("Address").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="激活日期">
                        <ItemTemplate>
                            <asp:Label ID="LabelRegisterDate" runat="server"
                                Text='<%# Bind("AuthoredDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />  
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="1"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager> 
        </div>
    </form>
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
</body>
</html>
