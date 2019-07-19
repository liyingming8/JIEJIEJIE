<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_ZhiJiuInfo.aspx.cs" Inherits="Admin_TB_SY_ZhiJiuInfo" %>
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
                <div class="div_Nav">
                <table class="table_Nav" >
                    <tr><td class="td1"> 
                         <asp:DropDownList ID="DDLField" runat="server">

                                             <asp:ListItem Value="ZhiJiuPiCi">制酒批次</asp:ListItem>
                                             <%--<asp:ListItem Value="ZhiJiuCheJian">ZhiJiuCheJian</asp:ListItem>
<asp:ListItem Value="ZhiJiuBanZu">ZhiJiuBanZu</asp:ListItem>
<asp:ListItem Value="ZhiJiuShiJian">ZhiJiuShiJian</asp:ListItem>--%> 
                                         </asp:DropDownList></td>
                        <td class="td_Nav">
                     包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" placeholder="请输入查找内容"  /> <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td>
                        <td class="td1">相关操作 : <input type="button" value="新增" class="btn btn-warning" onclick="openWinCenter('TB_SY_ZhiJiuInfoAddEdit.aspx?cmd=add', 600, 400, '制酒管理')" /></td></tr></table>
                    </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" Width="100%" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="ZhiJiuID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:TemplateField HeaderText="制曲批次">
                            <ItemTemplate>
                       
                                <asp:Label ID="LabelZhiQuID" runat="server" Text='<%# (Eval("ZhiQuID").Equals(null) ? "" : bzq.GetList(Convert.ToInt32(Eval("ZhiQuID"))).ZhiQuPiCi) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="制酒批次">
                            <ItemTemplate>
                                <asp:Label ID="LabelZhiJiuPiCi" runat="server" Text='<%# Bind("ZhiJiuPiCi") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="制酒车间">
                            <ItemTemplate>
                                <asp:Label ID="LabelZhiJiuCheJian" runat="server" Text='<%# Bind("ZhiJiuCheJian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="制酒班组">
                            <ItemTemplate>
                                <asp:Label ID="LabelZhiJiuBanZu" runat="server" Text='<%# Bind("ZhiJiuBanZu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="制酒时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelZhiJiuShiJian" runat="server" Text='<%# Bind("ZhiJiuShiJian") %>'></asp:Label>
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
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                   <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="20"   NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

            </div>
        </form>
         <script src="../include/js/jquery.min.js" type="text/javascript"></script> 
        <script type="text/javascript" src="../include/js/UploadImage.js"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>