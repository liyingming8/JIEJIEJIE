<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_ActivitySMInfoJoin.aspx.cs" Inherits="Admin_TJ_ActivitySMInfoJoin" %> 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="cc" Namespace="BL.Controls.ComboBox" Assembly="BL.Controls" %>
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
                <div class="topitem">
                    <asp:DropDownList runat="server" ID="ddl_departid" DataTextField="department" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddl_departid_SelectedIndexChanged">
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <cc:ComboBox  runat="server" DataTextField="CompName" Width="250px" DataValueField="CompID" ID="ddl_jingxiaoshang" AppendDataBoundItems="True" AutoPostBack="True" OnComboBoxChanged="ddl_jingxiaoshang_ComboBoxChanged">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                    </cc:ComboBox>
                </div>
                <div class="topitem"><span>起始时间</span></div>
                <div class="topitem">
                    <input id="txt_start" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div>
                <div class="topitem">
                    <input id="txt_end" runat="server" class="inputdatenew" onfocus="WdatePicker({isShowClear:false,readOnly:true})" />
                </div> 
                <div class="topitem"><span>瓶码</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
                <div class="topitem">
                    <asp:Button runat="server" ID="btn_createexcel" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="btn_createexcel_Click"/>
                </div>
                <%-- <div class="topitem">
                    <input type="button" class="btn btn-default btnyd" onclick="openWinCenter('TJ_SaoMaMapShow.aspx?startday='+txt_start.value+'&endday='+txt_end.value, 800, 600,'扫码信息')" value="地图显示"/>  
                </div>--%>
                <%--<div class="topitem">
                    <asp:Button runat="server" ID="btn_excel" CssClass="btn btn-info btnyd" Text="导出EXCEL" OnClick="btn_excel_Click"/>
                </div>--%>
            </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>  
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"  
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
                                <asp:Label ID="LabelLabelCode" runat="server" Text='<%# Bind("label") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="省份">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMProc" runat="server" Text='<%# Bind("sm_sheng") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="市">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMsj" runat="server" Text='<%# Bind("sm_shi") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="县">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMxj" runat="server" Text='<%# Bind("sm_xian") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="地址">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMAddress" runat="server" Text='<%# Bind("sm_address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="昵称">
                              <ItemTemplate>
                                  <asp:Label ID="Labelnickname" runat="server" Text='<%# Bind("xfznknm") %>'></asp:Label>   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelSMTime" runat="server" Text='<%# Bind("sm_time") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="经销商">
                             <ItemTemplate>
                                 <asp:Label ID="LabelJXS" runat="server" Text='<%# Bind("jxs") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="城市经理">
                            <ItemTemplate>
                                <asp:Label ID="label_city_manager" Text='<%# Bind("manger") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="终端店">
                            <ItemTemplate>
                                <asp:Label ID="label_terminal" runat="server" Text='<%# Bind("AcceptAgent") %>'></asp:Label>
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
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    </form>
</body>
</html>
