<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SY_YuanLiangInfo.aspx.cs" Inherits="Admin_TB_SY_YuanLiangInfo" %>
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
                <table class="table_Nav" style="width:60%">
                    <tr><td class="td1"> 
                         <asp:DropDownList ID="DDLField" runat="server">

                                             <asp:ListItem Value="原粮名称"></asp:ListItem>

                                             <asp:ListItem Value="单号">YLCaiGouDanHao</asp:ListItem>
                                             <%--<asp:ListItem Value="YLCaiGouShiJian">YLCaiGouShiJian</asp:ListItem>
<asp:ListItem Value="YLCaiGouShuLiang">YLCaiGouShuLiang</asp:ListItem>
<asp:ListItem Value="YLCaiGouDanWei">YLCaiGouDanWei</asp:ListItem>
<asp:ListItem Value="YLCaiGouRen">YLCaiGouRen</asp:ListItem>
<asp:ListItem Value="YLShenHeRen">YLShenHeRen</asp:ListItem>--%> 
                                         </asp:DropDownList></td>
                        <td class="td_Nav">
            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" placeholder="请输入查找内容"  /> <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td>
              <td class="td1">相关操作 : <input type="button" value="新增" class="btn btn-warning" onclick="openWinCenter('TB_SY_YuanLiangInfoAddEdit.aspx?cmd=add', 600, 400, '原料管理')" /></td></tr></table>
                </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" Width="100%" runat="server" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="YLID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:TemplateField HeaderText="原粮名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLMingCheng" runat="server" Text='<%# Bind("YLMingCheng") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="原粮产地">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLChanDI" runat="server" Text='<%# Bind("YLChanDI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="供应商">
                            <ItemTemplate>
                     
                                <asp:Label ID="LabelYLGongYingShangID" runat="server"    Text='<%# (Eval("YLGongYingShangID").Equals(null) ? "" : mgongyingshang.GetList(Convert.ToInt32(Eval("YLGongYingShangID"))).GYSMingCheng) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="采购单号">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLCaiGouDanHao" runat="server" Text='<%# Bind("YLCaiGouDanHao") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="采购时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLCaiGouShiJian" runat="server" Text='<%# Bind("YLCaiGouShiJian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="采购数量">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLCaiGouShuLiang" runat="server" Text='<%# Bind("YLCaiGouShuLiang") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="采购单位">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLCaiGouDanWei" runat="server" Text='<%# Bind("YLCaiGouDanWei") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="采购人">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLCaiGouRen" runat="server" Text='<%# Bind("YLCaiGouRen") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审核人">
                            <ItemTemplate>
                                <asp:Label ID="LabelYLShenHeRen" runat="server" Text='<%# Bind("YLShenHeRen") %>'></asp:Label>
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