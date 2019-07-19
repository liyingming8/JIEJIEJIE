<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_375SMinfo.aspx.cs" Inherits="Admin_TJ_375SMinfo" %> 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
        <div class="div_WholePage">
            <div class="topdiv"> 
                <div class="topitem"><span>起始时间</span></div>
                <div class="topitem">
                    <input id="txt_start" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div>
                <div class="topitem">
                    <input id="txt_end" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="LabelCode">标签号码</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                <div class="topitem">
                    <input type="button" class="btn btn-default btnyd" onclick="openWinCenter('TJ_SaoMaMapShow.aspx?startday='+txt_start.value+'&endday='+txt_end.value, 800, 600,'扫码信息')" value="地图显示"/>  
                </div>
                <div class="topitem">
                    <asp:Button runat="server" ID="btn_excel" CssClass="btn btn-info btnyd" Text="导出EXCEL" OnClick="btn_excel_Click"/>
                </div>
            </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>  
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="ID,UserID"
                      OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标签号码">
                            <ItemTemplate>
                                <asp:Label ID="LabelLabelCode" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="省份">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMProc" runat="server" Text='<%# Bind("SMProc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="市">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMsj" runat="server" Text='<%# Bind("SMsj") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="县">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMxj" runat="server" Text='<%# Bind("SMxj") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="地址">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMAddress" runat="server" Text='<%# Bind("SMAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="扫码时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMTime" runat="server" Text='<%# Bind("SMTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="客户">
                              <ItemTemplate>  
                                  <asp:HyperLink ID="hyperlinkuser" runat="server">详细</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate> 
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
                     </div> 
                          <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="12" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  NumericButtonCount="5" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页"></webdiyer:AspNetPager>  
            </ContentTemplate>
            <Triggers> 
            </Triggers>
        </asp:UpdatePanel> 
       </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    </form>
</body>
</html>
