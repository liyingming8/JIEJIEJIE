<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shangmengfanyong.aspx.cs" Inherits="Admin_shangmengfanyong" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %> 
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <title>商盟返佣</title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
                <table  class="tdbg"><tr><td>开始时间</td><td>
                                                                                                                <asp:TextBox ID="TextBox_StartDate" runat="server" CssClass="input5"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_StartDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                                                                                                            </td><td>结束时间</td><td>
                                                                                                                                  <asp:TextBox ID="TextBox_EndDate" runat="server" CssClass="input5"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_EndDate" Format="yyyy-MM-dd" runat="server"></cc2:CalendarExtender>
                                                                                                                              </td><td>所属商盟</td><td>
                                                                                                                                                    <asp:DropDownList ID="ComboBox_ShangMeng" runat="server" OnComboBoxChanged="ComboBox_ShangMeng_ComboBoxChanged">
                                                                                                                                                    </asp:DropDownList>
                                                                                                                                                </td><td>
                                                                                                                                                         <asp:Button ID="Button_query" runat="server" OnClick="Button_query_Click" Text="查找" Width="50px" />
                                                                                                                                                     </td></tr></table>
            </div>
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　  AutoGenerateColumns="False" DataKeyNames="OrderID,UserID,GoodsID" onpageindexchanging="GridView1_PageIndexChanging"  onrowcancelingedit="GridView1_RowCancelingEdit" onrowdatabound="GridView1_RowDataBound"  CssClass="GridViewStyle" PageSize="7" >
                <Columns>
                    <%--<asp:TemplateField HeaderText="收货地址">
                    <ItemTemplate>
                        <asp:Label ID="LabelUserAddress" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="邮编">
                    <ItemTemplate>
                        <asp:Label ID="LabelPostStamp" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="产品">                    
                        <ItemTemplate>
                            <asp:PlaceHolder ID="PH_GoodsInfo" runat="server"></asp:PlaceHolder>  
                        </ItemTemplate>
                    </asp:TemplateField>      
                    <asp:TemplateField HeaderText="订单状态">               
                        <ItemTemplate>
                            <asp:Label ID="LabelOrderStatusID" runat="server" Text='<%# ReturnOrderStatus(Eval("OrderStatusID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下单日期">                 
                        <ItemTemplate>
                            <asp:Label ID="LabelOrderDate" runat="server" Text='<%# Convert.ToDateTime(Eval("OrderNumber").ToString().Substring(0, 4) + "-" + Eval("OrderNumber").ToString().Substring(4, 2) + "-" + Eval("OrderNumber").ToString().Substring(6, 2) + " " + Eval("OrderNumber").ToString().Substring(8, 2) + ":" + Eval("OrderNumber").ToString().Substring(10, 2) + ":" + Eval("OrderNumber").ToString().Substring(12, 2)) %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="金额">                
                        <ItemTemplate>
                            <asp:Label ID="LabelShoulPayMoney" runat="server" Text='<%# Bind("ShoulPayMoney") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="折扣">                   
                        <ItemTemplate>
                            <asp:Label ID="LabelDiscount" runat="server" Text='<%# (decimal.Parse(Eval("DisCount").ToString()).Equals(10) ? "无" : Eval("DisCount") + "折") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>     
                    <asp:TemplateField HeaderText="订购人">                   
                        <ItemTemplate>
                            <asp:Label ID="LabelUserID" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="推荐盟友">
                        <ItemTemplate>
                            <asp:Label ID="labmengyou" ForeColor="Orange" Text='<%# ReturnMengyouName(Eval("IntroCompID").ToString()) %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实时佣金">                   
                        <ItemTemplate>
                            <asp:Label ID="LabelReturnMoney" ForeColor="Orange" runat="server" Text='<%# Bind("ActualReturnMoney") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>         
                    <asp:TemplateField HeaderText="备注">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_Remarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_Remarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
        </form>
    </body>
</html>