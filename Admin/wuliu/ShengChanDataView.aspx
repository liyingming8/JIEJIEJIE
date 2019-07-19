﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShengChanDataView.aspx.cs" Inherits="Admin_wuliu_ShengChanDataView" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
    <link href="../../include/MasterPage.css" rel="stylesheet" />
    <link href="../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class=" topdiv">
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                </asp:ScriptManager>
                <div class="topitem">
                    <span>包装时间:</span>
                </div>
                <div class=" topitem">
                    <asp:TextBox ID="TextBox_RukuDateBegin" runat="server" CssClass="inputsearch"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                </div>
                <div class="topitem">
                    <span>至</span>
                </div>
                <div class="topitem">
                    <asp:TextBox ID="TextBox_RukuDateEnd" runat="server" CssClass="inputsearch"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                </div>
                <div class="topitem">
                    <span>生产批次: </span>
                </div>

                <div class="topitem">

                    <asp:DropDownList ID="DDL_ShengChanPiCi" runat="server">
                        <asp:ListItem Text="全部..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem">
                    <span>产品: </span>
                </div>

                <div class="topitem">
                    <asp:DropDownList ID="ComboBox_ProductInfo" AppendDataBoundItems="true" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID">
                        <asp:ListItem Text="全部..." Selected="True" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"> <span>库房:</span> </div>
                    <div class="topitem">
                        <asp:DropDownList ID="ComboBox_StoreHouse" AppendDataBoundItems="true" runat="server" DataTextField="StoreHouseName" DataValueField="STID">
                            <asp:ListItem Text="全部..." Selected="True" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="topitem">
                        <asp:Button ID="Button_Search" runat="server" Text="查 询" OnClick="Button_Search_Click"  CssClass="btn btn-warning btnyd" /> 
                    </div>
             
                <div class="topitem">
                    <asp:Label ID="Label1" runat="server" Text="入库产品列表:" Visible="False"></asp:Label>
                </div>
            </div>
           
            <asp:GridView ID="GridView_RukuInfo" Width="292%" runat="server" CellPadding="2" OnPageIndexChanging="GridView_RukuInfo_PageIndexChanging" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" OnRowDataBound="GridView_RukuInfo_RowDataBound" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="入库批次">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>合计：</FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="入库时间">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("FHDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="入库数量">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="Label_jianshu_ft" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Products_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库房">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("StoreHouseName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    尚未查询到满足条件的入库记录!
                </EmptyDataTemplate>
                <RowStyle Wrap="False" Height="28px" />
                <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                <HeaderStyle BackColor="#004A66" ForeColor="White" Height="30px" HorizontalAlign="Left"
                    VerticalAlign="Middle" Wrap="False" Font-Size="13px" />
                <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
            </asp:GridView>
        </div>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="15" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging" NumericButtonCount="5"></webdiyer:AspNetPager>
            <script src="../../include/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
    <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script>

    </form>
</body>
</html>
