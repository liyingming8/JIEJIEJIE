<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_App_LabelModel_Info.aspx.cs" Inherits="Admin_TJ_App_LabelModel_Info" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_App_LabelModel_InfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 720,580,'套标信息')" /></div>
                <div class="topitem"><input id="inputcompid" runat="server" placeholder="全部" class="inputsearch"/></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="labelmodelvalue">关系值</asp:ListItem>
                        <asp:ListItem Value="labelmodeldiscription">描述</asp:ListItem> 
                        <asp:ListItem Value="remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" EnableTheming="False" EnableViewState="False">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="所属公司">
                            <ItemTemplate>
                                <asp:Label ID="Labelcompid" runat="server" Text='<%# BtjRegisterCompanys.GetList(int.Parse(Eval("compid").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="套标值">
                            <ItemTemplate>
                                <asp:Label ID="Labellabelmodelvalue" runat="server" Text='<%# Bind("labelmodelvalue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="套标关系">
                            <ItemTemplate>
                                <asp:Label ID="Labellabelmodeldiscription" runat="server" Text='<%# Bind("labelmodeldiscription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="更新时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelcreatetm" runat="server" Text='<%# Bind("createtm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                  <%--      <asp:TemplateField HeaderText="创建人">
                            <ItemTemplate>
                                <asp:Label ID="Labelcreateuserid" runat="server" Text='<%# BtjUser.GetList(int.Parse(Eval("createuserid").ToString())).LoginName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> --%>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" />
                        </asp:CommandField>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView> 
            </div><webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  NumericButtonCount="5" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页"></webdiyer:AspNetPager>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <input type="hidden" runat="server" id="hdcompid"/>
    </form>
</body>
</html>
