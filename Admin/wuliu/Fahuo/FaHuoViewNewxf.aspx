<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoViewNewxf.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoViewNewxf" %>

<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<%@ Register assembly="BL.Controls" namespace="BL.Controls.ComboBox" tagprefix="cc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <link href="../../../include/MasterPage.css" rel="stylesheet" />
    <link href="../../../include/easyui.css" rel="stylesheet" /> 
    <script src="../../../include/js/Wdate/WdatePicker.js"></script>
    <style type="text/css">
        .ComboBox_default table td {
            height: 1.98rem;
            line-height: 1.98rem;
        }
        .ComboBox_default .text_FW {
            height: 1.98rem !important;
            border-left: 0.08rem solid lightgrey !important;
            border-top: 0.08rem solid lightgrey !important;
            border-bottom: 0.08rem solid lightgrey !important;
        }
        .ComboBox_default .arrow_FW {
            height: 1.98rem !important;
            font-size: 12px !important;
            border-right: 0.08rem solid lightgrey !important;
            border-top: 0.08rem solid lightgrey !important;
            border-bottom: 0.08rem solid lightgrey !important;
        }
        .ComboBox_default .arrow_click_FW { 
            border-style: solid;
            width: 16px;
            height: 1.98rem !important;
            font-size: 12px !important;
            border-right: 0.08rem solid lightgrey !important;
            border-top: 0.08rem solid lightgrey !important;
            border-bottom: 0.08rem solid lightgrey !important;
        }
        .ComboBox_default .popup_FW li {
            height: 1.98rem !important;
        } 
        .ComboBox_default .popup_FW {
            border: 0.08rem solid lightgrey !important;
        } 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem"><%--<asp:DropDownList runat="server" ID="ddl_tm_type">
                    <asp:ListItem Selected="True" Value="1">发货时间</asp:ListItem>
                    <asp:ListItem Value="2">确认时间</asp:ListItem>
                    </asp:DropDownList>--%>
                    <span>发货日期</span>
                </div>
                <div class="topitem">
                    <input id="TextBox_RukuDateBegin" runat="server" style="width: 80px;" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="inputsearch" /> 
                </div>
                <div class="topitem"><span>至</span></div>
                <div class="topitem">
                    <input id="TextBox_RukuDateEnd" runat="server" style="width: 80px;" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="inputsearch" />
                </div>
                 <div class="topitem"><span>经销商</span></div>
                <div class="topitem"> 
                    <cc2:ComboBox ID="ComboBox_JXS" DataValueField="CompID" DataTextField="CompName" runat="server" RenderMode="ComboBoxSearch" AppendDataBoundItems="True" Width="250px">
                        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                    </cc2:ComboBox>
                </div> 
                <div class="topitem">
                    <asp:CheckBox runat="server" ID="check_all" Text="全部" Checked="False"/>
                </div>
                <div class="topitem">
                    <asp:Button ID="Button_Search" runat="server" Text="查 询" CssClass="btn btn-warning btnyd" OnClick="Button_Search_Click" ToolTip="分页显示仅供查看，数据无法被导出" />
                </div>
                <%-- <div class="topitem">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" Text="导出EXCEL" OnClick="Button1_Click" Enabled="False" ToolTip="需要导出数据时，请点击【查询】按钮【数据量大时会有些延时】" />
                </div>--%>
            </div>
            <div style="overflow-x: auto">
                <asp:GridView ID="GridView_RukuInfo" Width="100%" runat="server" CssClass="GridViewStyle"  AutoGenerateColumns="False" OnRowDataBound="GridView_RukuInfo_RowDataBound" DataKeyNames="fhkey,FHPiCi,KhDDH">
                    <Columns> 
                        <asp:TemplateField HeaderText="订单编号">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hplink_ordercode" Text='<%# Bind("KhDDH") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货经销商">
                            <ItemTemplate>
                                <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="发货批次">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                            </ItemTemplate>
                            <%--  <FooterTemplate>合计：</FooterTemplate>--%>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货时间">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("FHDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                     <%--   <asp:TemplateField HeaderText="发货库房">
                            <ItemTemplate>
                                <asp:Label ID="Label_storehouse" runat="server" Text='<%# bstorehouse.GetList(int.Parse(Eval("stid").ToString())).StoreHouseName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发货人">
                            <ItemTemplate>
                                <asp:Label ID="LabelFHR" runat="server" Text='<%# Bind("FHUserID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="发货件数">
                            <ItemTemplate>
                                <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label_jianshu_ft" runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="确认数量">
                            <ItemTemplate>
                                <asp:Label ID="Label_jxsconfirmnum" runat="server" Text='<%# agentcrim(Eval("fhkey").ToString()) %>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="异常"> 
                            <ItemTemplate>
                                <asp:HyperLink ID="hlinkerror" ForeColor="#009900" runat="server">详细</asp:HyperLink><asp:Label ID="lbprodnum" ForeColor="red" runat="server" Text='<%# "("+ error(Eval("fhkey").ToString())+")"  %>'></asp:Label> 
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

                <webdiyer:AspNetPager ID="AspNetPager1" ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging" CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
                <%--  <FooterTemplate>合计：</FooterTemplate>--%>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <script src="../../../js/jquery-1.7.1.js"></script>
        <script type="text/javascript" src="../../../include/js/jquery.min.js"></script>
        <script type="text/javascript" src="../../../include/js/UploadImage.js"></script>
        <script src="../../../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
