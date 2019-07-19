<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CommonBaseDate.aspx.cs" Inherits="Admin_wuliu_TJ_CommonBaseDate" %> 
<%@ Register TagPrefix="cc1" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.16598, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
  <div class="div_WholePage">
            <div class="topdiv"> 
                <div class="topitem">
                    <asp:DropDownList ID="ddlprodid" DataValueField="Infor_ID" DataTextField="Products_Name" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                    </asp:DropDownList>
                </div> 
                <div class="topitem">
                    <span>采集时间</span>
                </div>
                <div class="topitem"><asp:TextBox type="text" runat="server" style="width: 70px;" CssClass="inputsearch" id="startdate"/></div><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="startdate" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                <div class="topitem"><span>至</span></div>
                <div class="topitem"><asp:TextBox type="text" runat="server" style="width: 70px;" CssClass="inputsearch" id="enddate"/> 
                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="enddate" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                </div>
                 <div class="topitem">
                    <span>标签号码</span>
                </div>
                  <div class="topitem">
                     <input id="inputcode" class="inputsearch"  runat="server"/>
                </div> 
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" EnableViewState="False" runat="server"  OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" ViewStateMode="Disabled">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                    </Columns>
                     <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /> 
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                </asp:GridView>
                              <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15"  NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging"  CustomInfoSectionWidth="" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" CustomInfoHTML=""></webdiyer:AspNetPager> 
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
    <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </form>
</body>
</html>
