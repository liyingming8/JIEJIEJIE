﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Userqp.aspx.cs" Inherits="TJ_Userqp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" /> 
         <link href="../../include/easyui.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div class="div_WholePage">
                <div class="topdiv">
                    <div class="topitem"><asp:DropDownList ID="DDLField" runat="server"  > 
                                <asp:ListItem Value="LabelCode">标签序号</asp:ListItem>
                                <asp:ListItem Value="UserID">会员昵称</asp:ListItem> 
                            </asp:DropDownList></div>
                    <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /></div>
                    <div class="topitem"><span>上传时间</span></div>
                    <div class="topitem"><asp:TextBox ID="DateBegin"  class="inputsearch" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="DateBegin" Format="yyyy-MM-dd"    runat="server"></cc1:CalendarExtender></div>
                    <div class="topitem">包含:</div>
                    <div class="topitem">   <asp:TextBox ID="DateEnd" class="inputsearch" runat="server" ></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="DateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender></div>
                       <div class="topitem">  <%-- 发货地址：--%><asp:DropDownList ID="Litbox_address" DataTextField="Remarkes1" runat="server" Visible="false"></asp:DropDownList></div>
                       <div class="topitem">    <%--  原发货代理商：--%><asp:DropDownList ID="Litbox_agent" DataTextField="CompName" runat="server" Visible="false"></asp:DropDownList></div>
                      <div class="topitem"> <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="bt btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                      
                     </div> 
                <table  class="gridtable">
                    <tr runat="server" id="Cuser">
                    </tr>
                    <tr>
                        <td></td>

                    </tr>
                </table>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                                      AutoGenerateColumns="False" DataKeyNames="ID" 
                                      
                                      OnRowDeleting="GridView1_RowDeleting" 
                                       OnRowDataBound="GridView1_RowDataBound"
                                      CssClass="GridViewStyle" PageSize="30">
                            <Columns>

                                <asp:TemplateField HeaderText="标签序号">
                                    <ItemTemplate>
                                        <asp:Label ID="L1" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="扫码人">
                                    <ItemTemplate>
                                        <asp:Label ID="L2" runat="server" Text='<%# buser.GetList(int.Parse(Eval("UserID").ToString())).NickName %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="上传时间">
                                    <ItemTemplate>
                                        <asp:Label ID="L5" runat="server" Text='<%# Bind("SMTime", "{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label>
                               
                                    </ItemTemplate>
                                </asp:TemplateField>
                            
                                <asp:TemplateField HeaderText="扫码地址">
                                    <ItemTemplate>
                                        <asp:Label ID="L6" runat="server" Style="color: red;"  Text='<%# Bind("SMAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="销售区域">
                                    <ItemTemplate>
                                        <asp:Label ID="L4" runat="server" Text='<%# Bind("Remarkes1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="原发货代理商">
                                    <ItemTemplate>
                                        <asp:Label ID="L5" runat="server" Text='<%# Bind("CompName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                
                              <asp:TemplateField HeaderText="查询结果">
                                <ItemTemplate>
                                    <asp:Label ID="L4" runat="server"  Style="color: red;" Text='异常'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                            <PagerTemplate>
                                当前第:
                                <%--//((GridView)Container.NamingContainer)就是为了得到当前的控件--%>
                                <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView) Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                                页/共:
                                <%--//得到分页页面的总数--%>
                                <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView) Container.NamingContainer).PageCount %>"></asp:Label>
                                页
                                <%--//如果该分页是首分页，那么该连接就不会显示了.同时对应了自带识别的命令参数CommandArgument--%>
                                <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                                                Visible='<%#((GridView) Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                                                CommandName="Page" Visible='<%# ((GridView) Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                                <%--      //如果该分页是尾页，那么该连接就不会显示了--%>
                                <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                                                Visible='<%# ((GridView) Container.NamingContainer).PageIndex != ((GridView) Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                                <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                                Visible='<%# ((GridView) Container.NamingContainer).PageIndex != ((GridView) Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                                转到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="30px" Text='<%# ((GridView) Container.Parent.Parent).PageIndex + 1 %>' />页
                                <%--//这里将CommandArgument即使点击该按钮e.newIndex 值为3--%> 
                                <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                                                CommandName="Page" Text="GO" />
                            </PagerTemplate>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            
                            <PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="Blue" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView></div>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
            <asp:HiddenField ID="HF_UserID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js" type="text/javascript"></script> 
    </body>
</html>