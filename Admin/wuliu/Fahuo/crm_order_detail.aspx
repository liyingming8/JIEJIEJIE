<%@ Page Language="C#" AutoEventWireup="true" CodeFile="crm_order_detail.aspx.cs" Inherits="Admin_wuliu_Fahuo_crm_order_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
               <div class="topitem">
                   <span>【编号】</span><asp:Label runat="server" ID="lab_erpordercode" Text=""></asp:Label>
               </div>
                <div class="topitem">
                    <span>【经销商】</span><asp:Label runat="server" ID="lab_agentname" Text=""></asp:Label>
                </div> 
                <div class="topitem">
                    <span>【下单日期】</span>
                    <asp:Label runat="server" ID="lab_orderdate" Text=""></asp:Label>
                </div>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                    CssClass="GridViewStyle">
                    <Columns>
                         <asp:TemplateField HeaderText="物料编号">
                             <ItemTemplate>
                                 <asp:Label runat="server" ID="label_materialcode" Text='<%# Eval("materialcode") %>'></asp:Label>
                             </ItemTemplate> 
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="label_prodname" Text='<%# Eval("prodname") %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数量">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="label_orderquantity" Text='<%# Eval("orderquantity") %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView> 
            </div>
        </div>
        <script src="../../../include/js/jquery-1.7.1.js"></script> 
        <script src="../../../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
