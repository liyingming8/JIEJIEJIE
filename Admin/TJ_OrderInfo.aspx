<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_OrderInfo.aspx.cs" Inherits="Admin_TJ_OrderInfo" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
          <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />  
        <link href="../include/easyui.css" rel="stylesheet" />
        <link href="include/bootstrap.min.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
           <div class="div_WholePage">
                <%--<table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
<tr><td><b>搜索选项：</b></td>
<td>
 <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="OrderDate">订单时间</asp:ListItem>
<asp:ListItem Value="UnitPrice">UnitPrice</asp:ListItem>
<asp:ListItem Value="ShoulPayMoney">ShoulPayMoney</asp:ListItem>
<asp:ListItem Value="IsShowToUser">IsShowToUser</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>
</asp:DropDownList>
包含<input id="inputSearchKeyword" type="text" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td></tr></table>--%>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False"　 
                              AutoGenerateColumns="False" DataKeyNames="OrderID,UserID,GoodsID"
                              onpageindexchanging="GridView1_PageIndexChanging"   
                              onrowcancelingedit="GridView1_RowCancelingEdit"  
                              onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                              onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                              CssClass="GridViewStyle" onrowcommand="GridView1_RowCommand" PageSize="7" >
                    <Columns>               
                        <asp:TemplateField HeaderText="产品">                    
                            <ItemTemplate>
                                <table style="padding: 0px; margin: 0px; border: none; border-width: 0px; visibility: collapse;">
                                    <tr>
                                        <td>
                                            <asp:Image ID="GoodsImage" runat="server" Height="50px" ImageUrl='<%# ReturnGoodPicURL(Eval("GoodsID").ToString()) %>' Width="60px" />
                                        </td>
                                        <td>
                                            <asp:PlaceHolder ID="PH_GoodsInfo" runat="server"></asp:PlaceHolder>            
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>      
                        <asp:TemplateField HeaderText="订单状态">               
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderStatusID" runat="server" Text='<%# ReturnOrderStatus(Eval("OrderStatusID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下单日期">                 
                            <ItemTemplate>
                                <%--<asp:Label ID="LabelOrderDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderNumber").ToString().Substring(0,4)+"-"+Eval("OrderNumber").ToString().Substring(4,2)+"-"+Eval("OrderNumber").ToString().Substring(6,2)+" "+ Eval("OrderNumber").ToString().Substring(8,2)+":"+Eval("OrderNumber").ToString().Substring(10,2)+":"+Eval("OrderNumber").ToString().Substring(12,2))%>'></asp:Label>--%>
                                <asp:Label ID="LabelOrderDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelOrderNum" runat="server" Text='<%# Bind("OrderNum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="单价">                
                            <ItemTemplate>
                                <asp:Label ID="LabelUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText ="折扣">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelDiscount" runat="server" Text='<%# ((string.IsNullOrEmpty(Eval("DIsCount").ToString())||Eval("DisCount").ToString()=="10")) ? "无" : Convert.ToDecimal(Eval("DisCount").ToString())+"折" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="应付金额">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelShoulPayMoney" runat="server" Text='<%# Bind("ShoulPayMoney") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="订购人">                   
                            <ItemTemplate>
                                <table><tr><td><asp:Image ID="headerlogo" Width="50px" runat="server" /></td><td><asp:Label ID="LabelUserID" runat="server"></asp:Label></td></tr></table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="使用日期">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelUseDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderDate")).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="接单人">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelDeliveryUserID" Text='<%# ((!Eval("DeliveryUserID").Equals(DBNull.Value) && Eval("DeliveryUserID").ToString() != "0") ? buser.GetList(int.Parse(Eval("DeliveryUserID").ToString())).LoginName : "") %>'  runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="接单时间">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelDeliveryComfirmDate" Text='<%# Eval("DeliveryComfirmDate") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货单位">
                            <ItemTemplate>
                                <asp:Label ID="labmengyou" ForeColor="Orange" Text='<%# ((!Eval("DeliveryCompID").Equals(DBNull.Value) && Eval("DeliveryCompID").ToString() != "0") ? bcompany.GetList(int.Parse(Eval("DeliveryCompID").ToString())).CompName : "") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox_Remarks" TextMode="MultiLine"  Width="200px" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label_Remarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                   <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="10"   NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

            </div>
        </form>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
            <script src="../include/js/UploadImage.js" type="text/javascript"></script>
            <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>