<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_BaseClass.aspx.cs" Inherits="Admin_TJ_BaseClass" %> 
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
</head>
<body>
    <form id="form1" defaultbutton="BtnSearch0" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_BaseClassAddEdit.aspx?cmd=add', 400, 300, '基础类别维护')" /></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="CName">类型名称</asp:ListItem>
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch"/></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div> 
            </div> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate> 
                                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"
                                    AutoGenerateColumns="False" DataKeyNames="CID"  
                                    OnRowDeleting="GridView1_RowDeleting" 
                                    OnRowDataBound="GridView1_RowDataBound"
                                    CssClass="GridViewStyle" PageSize="18">
                                    <Columns> 
                                        <asp:TemplateField HeaderText="父级类型">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelParentID" runat="server" Text='<%# GetCnameByCID(Eval("ParentID").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="类型名称">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCName" runat="server" Text='<%# Bind("CName") %>' MaxLength="50"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelCName" runat="server" Text='<%# Bind("CName") %>'></asp:Label>
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
                                        <%--<asp:CommandField ShowEditButton="True" />--%>
                                        <asp:CommandField ShowDeleteButton="True">
                                            <ItemStyle  HorizontalAlign="Center" Width="50px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                </asp:GridView></div>
                                <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="10"  NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth="" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5"></webdiyer:AspNetPager> 
                </ContentTemplate> 
            </asp:UpdatePanel> 
        </div>
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
</body>
</html>
