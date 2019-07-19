<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_InfoType.aspx.cs" Inherits="Admin_TJ_InfoType" %>

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
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_InfoTypeAddEdit.aspx?cmd=add', 400, 300, '咨询类别编辑')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="TypeName">类别名称</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem> 
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False" DataKeyNames="IFTypeID"
                  OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="所属单位"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# ReturnCompName(Eval("CompID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="父级类型"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelParentID" runat="server" Text='<%# Bind("ParentID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="资讯类别"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注"> 
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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


            <%-- <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>--%>
        </div>
    </form> 
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
</body>
</html>
