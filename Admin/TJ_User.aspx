<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_User.aspx.cs" Inherits="Admin_TJ_User" %> 
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
                    <input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_UserAddEdit.aspx?cmd=add', 680, 580, '用户信息编辑')" />
                </div> 
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="LoginName">用户名</asp:ListItem>
                        <asp:ListItem Value="NickName">昵称</asp:ListItem>
                    </asp:DropDownList>
                </div> 
          <%--      <div class="topitem">
                       <asp:DropDownList runat="server" ID="ddlcompid"/>
                </div>--%>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
                
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate> 
                    <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  
                        AutoGenerateColumns="False" DataKeyNames="UserID,CompID"  
                        OnRowDeleting="GridView1_RowDeleting"  OnRowDataBound="GridView1_RowDataBound"
                        CssClass="GridViewStyle" PageSize="18">
                        <Columns>
                            <asp:TemplateField HeaderText="所属单位">
                                <ItemTemplate>
                                    <asp:Label ID="LabelCompID" runat="server" Text='<%# GetCompanyNameByID(Eval("CompID").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="系统角色">
                                <EditItemTemplate>
                                    <asp:DropDownList DataTextField="RoleName" DataValueField="RID" DataSource='<%# GetRoleInfo() %>' ID="ddl_role" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelRID" runat="server" Text='<%# ReturnRoleNameByRid(Eval("RID").ToString()) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用户名">
                                <ItemTemplate>
                                    <asp:Label ID="LabelLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="昵称">
                                <ItemTemplate>
                                    <asp:Label ID="LabelNickName" runat="server" Text='<%# Bind("NickName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="注册时间">
                                <ItemTemplate>
                                    <asp:Label ID="LabelRegisterDate" runat="server" Text='<%# Bind("RegisterDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="激活">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="radiolist" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="未激活" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="激活" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="试用" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="冻结" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LabelIsActived" runat="server" Text='<%# ReturnUserStatu(Eval("IsActived")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>  
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                    </asp:GridView></div>
                    <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  NumericButtonCount="5" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页"></webdiyer:AspNetPager>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>  
    </form>
    </body>
</html>
