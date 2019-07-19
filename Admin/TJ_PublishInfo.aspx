<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_PublishInfo.aspx.cs" Inherits="Admin_TJ_PublishInfo" %> 
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
                            <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_PublishInfoAddEdit.aspx?cmd=add', 700, 700, '内容编辑')" /></div>
                            <div class="topitem"><asp:DropDownList ID="DDLField" runat="server">
                                <asp:ListItem Value="Title">标题</asp:ListItem>
                                <asp:ListItem Value="Contents">内容</asp:ListItem>
                                <asp:ListItem Value="Remarks">备注</asp:ListItem>
                            </asp:DropDownList></div> 
                            <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div> 
                            <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /> </div>  
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="IFID" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" PageSize="20">
                <Columns>
                    <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="../Admin/image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="资讯类别">
                        <ItemTemplate>
                            <asp:Label ID="LabelCID" runat="server" Text='<%# ReturnInfoTypeName(Eval("CID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="主题"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="相关链接"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelLinkURLString" runat="server" ToolTip='<%# Bind("LinkURLString") %>' Text='<%# (Eval("LinkURLString").ToString().Length>5?Eval("LinkURLString").ToString().Substring(0,5): Eval("LinkURLString").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="热点">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox_IsHot" runat="server" Enabled="false" Checked='<%# Bind("IsHot") %>' />
                        </ItemTemplate> 
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="IFID" ItemStyle-CssClass="btn btn-default" Text="管理" DataNavigateUrlFormatString="TJ_PublishImageInfo.aspx?IFID={0}" HeaderText="图片" />
                    <asp:TemplateField HeaderText="备注"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# (Eval("Remarks").ToString().Length > 30 ? Eval("Remarks").ToString().Substring(0, 30) : Eval("Remarks").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- 
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle CssClass="btn btn-default" HorizontalAlign="Center" />
                    </asp:CommandField>--%>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <%--<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>--%>
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
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
