<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_QuerLabelCodeJG.aspx.cs" Inherits="Admin_TJ_QuerLabelCodeJG" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr>
                        <td nowrap="noWrap" align="left">起始时间:</td>
                        <td colspan="4" nowrap="noWrap" class="tdbg" >
                            <asp:TextBox ID="TextBox_RukuDateBegin" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                            至
                            <asp:TextBox ID="TextBox_RukuDateEnd" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                        </td>
                  
                        <td class="">
                            <%-- <asp:DropDownList ID="DDLField" runat="server">
                            <asp:ListItem Value="labelcode">标签序号</asp:ListItem>
                        </asp:DropDownList>--%>标签序号<input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" />查询超过累计次数<input id="inputSearchKeywordcishu" type="text" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table> 
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="ID"
                               OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                   

                        <asp:TemplateField HeaderText="上传人">
                            <ItemTemplate>
                                <asp:Label ID="Labellabelcode" runat="server" Text='<%# Bind("remarks2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="上传时间">
                            <ItemTemplate>
                                <asp:Label ID="Labelfirshangchuantime" runat="server" Text='<%# Bind("firshangchuantime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    
                        <asp:TemplateField HeaderText="门店">
                            <ItemTemplate>
                                <asp:Label ID="Labeladdress" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelProID" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ImageField DataImageUrlField="image" HeaderText="产品图像" ItemStyle-Width="100px" ControlStyle-Width="100px">                   
                        </asp:ImageField>

                        <asp:TemplateField HeaderText="执法部门">
                            <ItemTemplate>
                                <asp:Label ID="Labelnum" runat="server" Text='<%# Bind("remarks3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--  <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# XiangXiLinkString(Eval("ID").ToString()) %>' Text="详细1"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <%-- <asp:TemplateField HeaderText="详细">
                        <ItemTemplate>
                            <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# JiageLinkString(Eval("ID").ToString()) %>' Text='详细'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView></div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True"  runat="server"></asp:ScriptManager>
            </div>
        </form>
    </body>
</html>