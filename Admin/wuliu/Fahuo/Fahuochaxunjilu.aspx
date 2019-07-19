<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fahuochaxunjilu.aspx.cs" Inherits="Admin_wuliu_fahuo_Fahuochaxunjilu" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
        <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="../../../include/easyui.css" rel="stylesheet" />
        <link href="../../../include/bootstrap.min.css" rel="stylesheet" />
            <style type="text/css"> 
            .ComboBox_default {
                height: 25px !important;
                line-height: 25px !important; 
            }
             .ComboBox_default .arrow_FW {
                 height: 25px !important;
                 line-height: 25px !important;
                 -ms-background-size: 100% 100% !important;
                 background-size: 100% 100% !important;
                  border: 1px solid #bbb !important;
                  margin-bottom: 4px !important;
             }
             .ComboBox_default .text_FW {
                 height: 25px !important;
                 line-height: 25px !important;
             }
            .ComboBox_default .popup_FW li {
                line-height: 20px !important;
                height: 20px !important;
            } 
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem"><span>起始时间</span></div>
                <div class="topitem">
                    <asp:TextBox ID="TextBox_RukuDateBegin" runat="server" CssClass="input5"></asp:TextBox>
                    <cc1:calendarextender runat="server" id="CalendarExtender1" targetcontrolid="TextBox_RukuDateBegin" format="yyyy-MM-dd" /></div>
                <div class="topitem"><span>至</span></div>
                <div class="topitem">
                    <asp:TextBox ID="TextBox_RukuDateEnd" runat="server" CssClass="input5"></asp:TextBox>
                    <cc1:calendarextender id="CalendarExtender2" targetcontrolid="TextBox_RukuDateEnd" format="yyyy-MM-dd" runat="server"></cc1:calendarextender>
                </div>
                <div class="topitem"><span>经销商</span></div>
                <div class="topitem">
                    <asp:DropDownList id="ComboBox_DaiLiShangID" runat="server" appenddatabounditems="true" datatextfield="CompName" datavaluefield="CompID" rendermode="ComboBoxSearch">
                                    <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                </div>
                <div class="topitem"><span>产品</span></div>
                <div class="topitem">
                    <asp:DropDownList id="ComboBox_ProInfo" runat="server" appenddatabounditems="true" datatextfield="Products_Name" datavaluefield="Infor_ID" rendermode="ComboBoxSearch">
                                    <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                </div>
                <div class="topitem"><span>货仓</span></div>
                <div class="topitem">
                    <asp:DropDownList id="ComboBox_StoreHouse" runat="server" appenddatabounditems="true" datatextfield="StoreHouseName" datavaluefield="STID" rendermode="ComboBoxSearch">
                                    <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                </div>
                <div class="topitem">
                   <div class="topitem"><asp:Button ID="Button2" runat="server" Text="查 询" CssClass="btn btn-warning btnyd" OnClick="Button_Search_Click"  ToolTip="分页显示仅供查看，数据无法被导出" /></div>
                <div class="topitem">
<%--                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default btnyd" Text="导出EXCEL" OnClick="Button1_Click" Enabled="False" ToolTip="需要导出数据时，请点击【查询】按钮【数据量大时会有些延时】" />--%></div>
            </div>

            <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" DataKeyNames="userid" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" AutoGenerateColumns="False">
                <Columns>
                       <asp:TemplateField HeaderText="发货批次">
                        <ItemTemplate>
                            <asp:Label ID="fhpici" runat="server" Text='<%# Bind("fhpici") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="产品名称">
                        <ItemTemplate>
                            <asp:Label ID="pid" runat="server" Text='<%#  proname( Eval("pid").ToString())%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="发货时间">
                        <ItemTemplate>
                            <asp:Label ID="tm" runat="server" Text='<%# Bind("tm") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货经销商">
                        <ItemTemplate>
                            <asp:Label ID="LabelAddressInfo" runat="server" Text='<%# bollRegisterCompanys.GetListsByFilterString("Agent_Code="+int.Parse( Eval("tocompid").ToString()))[0].CompName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label ID="xiangnumber" runat="server" Text='<%# Bind("xiangnumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="仓库">
                        <ItemTemplate>
                            <asp:Label ID="frstoid" runat="server" Text='<%# bllsthose.GetListsByFilterString("StoreHouseCode="+Eval("frstoid"))[0].StoreHouseName%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="发货人">
                        <ItemTemplate>
                            <asp:Label ID="LabelNickName" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div> 
        </div> 
          <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth="" ></webdiyer:AspNetPager> </div>
            </ContentTemplate>
        </asp:UpdatePanel>
       
    </form>
</body>
</html>
