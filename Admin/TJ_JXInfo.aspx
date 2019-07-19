<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_JXInfo.aspx.cs" Inherits="Admin_TJ_JXInfo" %>

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
                    <input type="button" value="新增" class="btn btn-warning" onclick="openWinCenter('TJ_JXInfoAddEdit.aspx?cmd=add', 500, 400, '奖项设置')" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <%--<asp:ListItem Value="JxID">JxID</asp:ListItem>--%>
                        <asp:ListItem Value="JxName">奖项名称</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                        <%--<asp:ListItem Value="JxGrade">奖项等级</asp:ListItem>--%>
                        <%--<asp:ListItem Value="JxContent">JxContent</asp:ListItem>
<asp:ListItem Value="SJname">SJname</asp:ListItem>
<asp:ListItem Value="SHname">SHname</asp:ListItem>
<asp:ListItem Value="CJtime">CJtime</asp:ListItem>
<asp:ListItem Value="DelFlag">DelFlag</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch1" runat="server" Text="导出Excel数据" class="btn btn-warning btnyd" OnClick="BtnSearch2_Click" />
                </div> 
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="JxID"
                  OnRowDeleting="GridView1_RowDeleting"   OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="所属活动">
                        <ItemTemplate>
                            <asp:Label ID="LabelLAID" runat="server" Text='<%# blotteryactivity.GetList(Convert.ToInt32(Eval("LAID"))).LotteryActivityName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖项名称">
                        <ItemTemplate>
                            <asp:Label ID="LabelJxName" runat="server" Text='<%# Bind("JxName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="奖品">
                        <ItemTemplate>
                            <asp:Label ID="LabelJxContent" runat="server" Text='<%# Bind("JxContent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="布奖人">
                        <ItemTemplate>
                            <asp:Label ID="LabelSJname" runat="server" Text='<%# Eval("SJname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="设定数量">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtOrderNum" CssClass="input4" runat="server" Text='<%# Bind("OrderNum") %>' MaxLength="10"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelOrderNum" runat="server" Text='<%# Bind("OrderNum") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' MaxLength="16"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
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
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
