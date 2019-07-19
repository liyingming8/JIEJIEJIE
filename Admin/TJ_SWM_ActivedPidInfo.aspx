<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SWM_ActivedPidInfo.aspx.cs" Inherits="Admin_TJ_SWM_ActivedPidInfo" %> 
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div class="div_WholePage">
            <div class="topdiv">  
                    <div class="topitem">
                        <span>激活日期</span>
                    </div> 
                <div class="topitem">
                    <input id="inputstartdate"  type="text" style="width: 5rem;" runat="server" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="inputsearch" />
                    </div>
                <div class="topitem">
                    <span>至</span>
                </div>
                 <div class="topitem">
                    <input id="inputenddate" type="text" style="width: 5rem;" onfocus="WdatePicker({isShowClear:false,readOnly:true})" runat="server" class="inputsearch" />
                    </div>
                <div class="topitem" id="foradmin" Visible="False" runat="server">
                    <input id="inputcompid" runat="server" class="inputsearch p60" placeholder="点击指定三维码客户"/>
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
            </div> 
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" runat="server" Width="100%" EnableViewState="False" AutoGenerateColumns="False" DataKeyNames="pid,product_id" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns> 
                    <asp:TemplateField HeaderText="批次">
                        <ItemTemplate>
                            <asp:Label ID="LabelGainName" runat="server" Text='<%# Bind("pid") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lab_activenum" Text='<%# Bind("qty") %>'></asp:Label> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlinkproduct" runat="server"><%# (Eval("product_id").ToString().Equals("0")?"绑定":ReturnGoodsNameByWlprid(Eval("product_id").ToString())) %></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="功能模块">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlinkmodules" runat="server">查看</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="溯源信息">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlinksuyuan" runat="server">编辑</asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="激活时间">
                        <ItemTemplate>
                            <asp:Label ID="LabelCount" runat="server" Text='<%# Bind("active_time") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="功能模块">
                        <ItemTemplate>
                            <input id="btn_add_packagefunction" type="button" runat="server" value="增加" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>   
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
        </div>
        <input id="hd_compid" runat="server" type="hidden" value="0"/>
        <script src="../include/js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script> 
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    </form>
</body>
</html>
