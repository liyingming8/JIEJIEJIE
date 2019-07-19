<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_GuanZhuangInfo.aspx.cs" Inherits="Admin_TB_SY_GuanZhuangInfo" %>
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
                <table class="table_Nav" style="width:90%">
                    <tr><td class="td1"> 
                         <asp:DropDownList ID="DDLField" runat="server"> 
                                             <asp:ListItem Value="DaYangJiuPICI">DaYangJiuPICI</asp:ListItem>
                                             <asp:ListItem Value="GuanZhuangCheJian">GuanZhuangCheJian</asp:ListItem>
                                             <asp:ListItem Value="GuanZhuangChanXian">GuanZhuangChanXian</asp:ListItem>
                                             <asp:ListItem Value="GuanZhuangBanZu">GuanZhuangBanZu</asp:ListItem>
                                             <asp:ListItem Value="ShengChanPICI">ShengChanPICI</asp:ListItem>
                                             <asp:ListItem Value="GuanZhuangShiJian">GuanZhuangShiJian</asp:ListItem>
                                             <asp:ListItem Value="GuanZhuangXianZhiJianYuan">GuanZhuangXianZhiJianYuan</asp:ListItem>
                                             <asp:ListItem Value="GuanZhuangXianZhiJianShiJian">GuanZhuangXianZhiJianShiJian</asp:ListItem> 
                                         </asp:DropDownList></td>
                         <td class="td_Nav">
                     包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" placeholder="请输入查找内容"  /> <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td>
                        <td class="td1">相关操作 : <input type="button" value="新增" class="btn btn-warning" onclick="openWinCenter('TB_SY_GuanZhuangInfoAddEdit.aspx?cmd=add', 600, 400, '灌装管理')" /></td></tr></table>
                    </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="GuanZhuangID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                
                        <asp:TemplateField HeaderText="GouTiaoID">
                            <ItemTemplate>
                                <asp:Label ID="LabelGouTiaoID" runat="server" Text='<%# Bind("GouTiaoID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="大样酒批次">
                            <ItemTemplate>
                                <asp:Label ID="LabelDaYangJiuPICI" runat="server" Text='<%# Bind("DaYangJiuPICI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GuanZhuangCheJian">
                            <ItemTemplate>
                                <asp:Label ID="LabelGuanZhuangCheJian" runat="server" Text='<%# Bind("GuanZhuangCheJian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GuanZhuangChanXian">
                            <ItemTemplate>
                                <asp:Label ID="LabelGuanZhuangChanXian" runat="server" Text='<%# Bind("GuanZhuangChanXian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GuanZhuangBanZu">
                            <ItemTemplate>
                                <asp:Label ID="LabelGuanZhuangBanZu" runat="server" Text='<%# Bind("GuanZhuangBanZu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ShengChanPICI">
                            <ItemTemplate>
                                <asp:Label ID="LabelShengChanPICI" runat="server" Text='<%# Bind("ShengChanPICI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GuanZhuangShiJian">
                            <ItemTemplate>
                                <asp:Label ID="LabelGuanZhuangShiJian" runat="server" Text='<%# Bind("GuanZhuangShiJian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GuanZhuangXianZhiJianYuan">
                            <ItemTemplate>
                                <asp:Label ID="LabelGuanZhuangXianZhiJianYuan" runat="server" Text='<%# Bind("GuanZhuangXianZhiJianYuan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GuanZhuangXianZhiJianShiJian">
                            <ItemTemplate>
                                <asp:Label ID="LabelGuanZhuangXianZhiJianShiJian" runat="server" Text='<%# Bind("GuanZhuangXianZhiJianShiJian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks1">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks1" runat="server" Text='<%# Bind("Remarks1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks2">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks2" runat="server" Text='<%# Bind("Remarks2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks3">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks3" runat="server" Text='<%# Bind("Remarks3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks4">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks4" runat="server" Text='<%# Bind("Remarks4") %>'></asp:Label>
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