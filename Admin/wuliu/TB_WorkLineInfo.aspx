<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_WorkLineInfo.aspx.cs" Inherits="Admin_TB_WorkLineInfo" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
             
                   <div class="topitem"><input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TB_WorkLineInfoAddEdit.aspx?cmd=add', 450, 300, '生产线')" /></div> 
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="WorkLineName">生产线</asp:ListItem>
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" placeholder="请输入查找内容" /></div>
               <div class="topitem">  <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                 
            </div>
            <div style="overflow-x: auto">
                       <asp:GridView ID="GridView1" EnableViewState="False" runat="server" AutoGenerateColumns="False" DataKeyNames="WLID"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <%--<asp:HyperLinkField DataNavigateUrlFields="WLID" DataNavigateUrlFormatString="TB_WorkLineInfoAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />--%>
                        <asp:TemplateField HeaderText="厂房（车间）">
                            <ItemTemplate>
                                <asp:Label ID="LabelWSID" runat="server" Text='<%# bworkshop.GetList(Convert.ToInt32(Eval("WSID"))).Workshop %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="生产线">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtWorkLineName" runat="server" Text='<%# Bind("WorkLineName") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelWorkLineName" runat="server" Text='<%# Bind("WorkLineName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />
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
         <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging" NumericButtonCount="5"></webdiyer:AspNetPager>
        </div>

    </form>
     <script src="../../../include/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../include/js/UploadImage.js"></script>
    <script src="../../../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
