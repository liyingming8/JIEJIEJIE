<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AdInfo.aspx.cs" Inherits="Admin_TJ_AdInfo" %>

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
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv"> 
                <div class="topitem">
                            <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_AdInfoAddEdit.aspx?cmd=add', 700, 400, '广告位编辑')" /></div>
                            <div class="topitem"><asp:DropDownList ID="DDLField" runat="server">
                                <asp:ListItem Value="ADName">广告名称</asp:ListItem>
                            </asp:DropDownList> </div>
                            
                            <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                            <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /> </div>
                
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False"  runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ADID"
                  OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <%-- <asp:HyperLinkField DataNavigateUrlFields="ADID" DataNavigateUrlFormatString="TJ_AdInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />--%>
                    <%-- <asp:TemplateField HeaderText="ADID">
                    <EditItemTemplate>
                    <asp:TextBox ID="txtADID" runat="server"  MaxLength="4"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LabelADID" runat="server" Text='<%# Bind("ADID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="广告名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtADName" runat="server" Text='<%# Bind("ADName") %>' MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelADName" runat="server" Text='<%# Bind("ADName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="媒体类别">
                        <ItemTemplate>
                            <asp:Label ID="LabelMediaType" runat="server" Text='<%# btj_base.GetList(int.Parse(Eval("MediaType").ToString())).CName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="宽度">
                        <ItemTemplate>
                            <asp:Label ID="LabelMWidth" runat="server" Text='<%# Bind("MWidth") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="高度">
                        <ItemTemplate>
                            <asp:Label ID="LabelMHeight" runat="server" Text='<%# Bind("MHeight") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="所属模块">
                        <ItemTemplate>
                            <asp:Label ID="LabelSiteID" runat="server" Text='<%#sit_map.GetList(int.Parse(Eval("SiteID").ToString())).PageName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CommandField ShowEditButton="True" />--%>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                
                
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15"  NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
            <%--   <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>--%>
        </div>
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
