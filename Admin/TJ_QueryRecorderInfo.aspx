<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_QueryRecorderInfo.aspx.cs"
    Inherits="Admin_TJ_QueryRecorderInfo" %>

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
                        <asp:ListItem Value="LabelNumber">标签号码</asp:ListItem>
                        <asp:ListItem Value="QueryDate">查询日期</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd"
                        OnClick="BtnSearch0_Click" />
                </div>

            </div>
            <%--<div class="div_WholePage">
            <div class="div_Nav">
                <table class="table_Nav">
                    <tr>
                        <td class="td1">
                            <asp:DropDownList ID="DDLField" runat="server">
                                <asp:ListItem Value="LabelNumber">标签号码</asp:ListItem>
                                <asp:ListItem Value="QueryDate">查询日期</asp:ListItem>
                            </asp:DropDownList></td>
                        <td class="td_Nav">包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"
                            placeholder="请输入查找内容" />
                            <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" />
                        </td>
                    </tr>
                </table>
            </div>--%>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False"
                    AutoGenerateColumns="False" DataKeyNames="QRID"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                    OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <%-- <asp:TemplateField HeaderText="所属公司">
                     <ItemTemplate>
                        <asp:Label ID="LabelTJCOMPID" runat="server" Text='<%# Bind("TJCOMPID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="标签号码">
                            <ItemTemplate>
                                <asp:Label ID="LabelLabelNumber" runat="server" Text='<%# Bind("LabelNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查询用户">
                            <ItemTemplate>
                                <asp:Label ID="LabelUserID" runat="server" Text='<%# ReturnUserName(Eval("UserID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查询时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelQueryDate" runat="server" Text='<%# Bind("QueryDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
            </div>
            <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"
                PageSize="20" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList"
                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText=""
                LastPageText="" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"
                CustomInfoHTML="" CustomInfoSectionWidth="">
            </webdiyer:AspNetPager>

        </div>
    </form>
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
