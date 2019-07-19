﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoViewNewxin.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoViewNewxin" %>  
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title></title>
     <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
            <link href="../../../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_WholePage">     
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>
            <table>
            <tr>
                <td nowrap="noWrap" align="left">
                    起始时间:</td>
                <td colspan="6" nowrap="noWrap">
                    <asp:TextBox ID="TextBox_RukuDateBegin" runat="server"></asp:TextBox>&nbsp;<cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                    至
                    <asp:TextBox ID="TextBox_RukuDateEnd" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                </td>
            </tr>
               <tr>
                   <td align="left" nowrap="nowrap">
                    代理商:</td>
                   <td nowrap="nowrap" >
                       <asp:DropDownList ID="ComboBox_DaiLiShangID" runat="server" AppendDataBoundItems="true"  DataTextField="CompName" DataValueField="CompID">
                           <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td nowrap="nowrap" >
                    产 品:</td>
                   <td nowrap="nowrap" >
                       <asp:DropDownList ID="ComboBox_ProInfo" runat="server" AppendDataBoundItems="true"  DataTextField="Products_Name" DataValueField="Infor_ID">
                           <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td nowrap="nowrap"  >
                       货仓:</td>
                   <td nowrap="nowrap" >
                       <asp:DropDownList ID="ComboBox_StoreHouse" runat="server" AppendDataBoundItems="true"  DataTextField="StoreHouseName" DataValueField="STID" >
                           <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                       </asp:DropDownList>
                    </td>
                   <td nowrap="nowrap">
                    <asp:Button ID="Button_Search" runat="server" Text="查 询" OnClick="Button_Search_Click" Width="60px" /></td>
                   <td>                            <asp:Button ID="Button1" runat="server" CssClass="inputbutton" Text="导出EXCEL" OnClick="Button1_Click" /></td>
               </tr>
            </table> 
        <table  width="100%">
            <tr >
                <td style=" height: 33px">
                    <asp:Label ID="Label1" runat="server" Text="发货产品列表:" ></asp:Label></td>
            </tr>
            <tr ><td style=" height: 240px; width:100%" nowrap="noWrap" valign="top"> 
                <asp:GridView ID="GridView_RukuInfo" Width="100%" runat="server" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left"  PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="GridView_RukuInfo_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="发货批次">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>合计：</FooterTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="StoreHouseName" HeaderText="发货库房" />
                               <asp:TemplateField HeaderText="产品名称">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Products_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="件数">                              
                                <ItemTemplate>
                                    <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="Label_jianshu_ft" runat="server" ></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="发货时间">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FHDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="收货经销商">
                                <ItemTemplate>
                                    <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                      
                           
                         
                            <asp:TemplateField HeaderText="发货人">                                
                                <ItemTemplate>
                                    <asp:Label ID="LabelFHR" runat="server" Text='<%# Bind("FHUserID") %>'></asp:Label>
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
                    </asp:GridView></td></tr></table>
    </div>
    </form>
</body>
</html>