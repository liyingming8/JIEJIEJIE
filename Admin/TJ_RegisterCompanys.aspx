<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RegisterCompanys.aspx.cs" Inherits="Admin_TJ_RegisterCompanys" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="renderer" content="webkit"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
  <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0"/>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
 </head>
<body> 
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_RegisterCompanysAddEdit.aspx?cmd=add', 680, 700, '公司信息')" /></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="CompName">单位名称</asp:ListItem>
                        <asp:ListItem Value="LegalPerson">法人</asp:ListItem>
                        <asp:ListItem Value="Address">地址</asp:ListItem>
                        <asp:ListItem Value="TelNumber">电话</asp:ListItem>
                        <asp:ListItem Value="FaxNumber">传真</asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                
            </div>

            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="false"
                AutoGenerateColumns="False" DataKeyNames="CompID"
                OnRowDataBound="GridView1_RowDataBound"
                CssClass="GridViewStyle" PageSize="18" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <%-- <asp:HyperLinkField DataNavigateUrlFields="CompID" DataNavigateUrlFormatString="TJ_RegisterCompanysAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />--%>
             <%--       <asp:TemplateField HeaderText="公司类别">
                        <ItemTemplate>
                            <asp:Label ID="LabelCompTypeID" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("CompTypeID").ToString(), false, false) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="公司名称">
                        <ItemTemplate>
                            <asp:Label ID="LabelCompName" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
               <%--     <asp:TemplateField HeaderText="产品类别">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("ProductTypeID").ToString(), false, false) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="法人">
                        <ItemTemplate>
                            <asp:Label ID="LabelLegalPerson" runat="server"
                                Text='<%# Bind("LegalPerson") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
            <%--        <asp:TemplateField HeaderText="城市">
                        <ItemTemplate>
                            <asp:Label ID="LabelCTID" runat="server" Text='<%# comfun.ReturnBaseClassName(Eval("CTID").ToString(), true, false) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="地址">
                        <ItemTemplate>
                            <asp:Label ID="LabelAddress" ToolTip='<%#Eval("Address").ToString() %>' runat="server" Text='<%# (Eval("Address").ToString().Length>10?Eval("Address").ToString().Substring(0,10):Eval("Address").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="传真">
                        <ItemTemplate>
                            <asp:Label ID="LabelFaxNumber" runat="server" Text='<%# Bind("FaxNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="电话">
                        <ItemTemplate>
                            <asp:Label ID="LabelTelNumber" runat="server" Text='<%# Bind("TelNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="电子邮箱">
                        <ItemTemplate>
                            <asp:Label ID="LabelZhuCeZiJin" runat="server" Text='<%# Bind("EMail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="注册资金">
                        <ItemTemplate>
                            <asp:Label ID="LabelEMail" runat="server" Text='<%# Bind("ZhuCeZiJin") %>'></asp:Label>万
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="注册日期">
                        <ItemTemplate>
                            <asp:Label ID="LabelRegisterDate" runat="server"
                                Text='<%# Bind("RegisterDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <%--    <asp:TemplateField HeaderText="帐号">
                        <ItemTemplate>
                            <asp:Label ID="LabelAccountNumber" runat="server"
                                Text='<%# Bind("AccountNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle CssClass="btn btn-default" HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" /> 
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
        </div>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
        <asp:HiddenField ID="HF_TempAgentCompIDString" runat="server" />
        <asp:HiddenField ID="HF_TempChildCompIDString" runat="server" />  
    </form>
</body>
</html>
