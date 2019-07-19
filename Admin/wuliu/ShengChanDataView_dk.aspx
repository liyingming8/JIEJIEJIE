<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShengChanDataView_dk.aspx.cs" Inherits="Admin_wuliu_ShengChanDataView_dk" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
        <link href="../../include/MasterPage.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
               <div class="div_WholePage"> 
                <div class="topdiv">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                    </asp:ScriptManager>
                    <div class="topitem"><span>包装时间</span></div>
                    <div class="topitem"> <asp:TextBox ID="TextBox_RukuDateBegin" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                                </div>
                    <div class="topitem"><span>至</span></div>
                    <div class="topitem"><asp:TextBox ID="TextBox_RukuDateEnd" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender></div>
                    <div class="topitem"> 生产批次</div>
                    <div class="topitem"><asp:DropDownList ID="DDL_ShengChanPiCi" runat="server">
                                    <asp:ListItem Text="全部..." Value="0"></asp:ListItem>
                                </asp:DropDownList></div>
                    <div class="topitem"><span>产品</span></div>
                    <div class="topitem"><asp:DropDownList ID="ComboBox_ProductInfo" AppendDataBoundItems="true" runat="server"   DataTextField="Products_Name" DataValueField="Infor_ID">
                                    <asp:ListItem Text="全部..." Selected="True" Value="0"></asp:ListItem>
                                </asp:DropDownList></div>
                    <div class="topitem"><span>库房</span></div>
                    <div class="topitem">  <asp:DropDownList ID="ComboBox_StoreHouse" AppendDataBoundItems="true" runat="server"   DataTextField="StoreHouseName" DataValueField="STID">
                                    <asp:ListItem Text="全部..." Selected="True" Value="0"></asp:ListItem>
                                </asp:DropDownList></div>
                    <div class="topitem"><asp:Button ID="Button_Search" runat="server" Text="查 询" OnClick="Button_Search_Click" Width="60px" /></div> 
                    </div>
                    <asp:GridView ID="GridView_RukuInfo"   runat="server" CellPadding="2" CssClass="GridViewStyle" OnPageIndexChanging="GridView_RukuInfo_PageIndexChanging"   CaptionAlign="Left"  PageSize="20" ShowFooter="True"  OnRowDataBound="GridView_RukuInfo_RowDataBound" AutoGenerateColumns="False" Caption="产品信息列表">
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
                                                                                                              <asp:Label ID="Label_jianshu_ft" runat="server" ></asp:Label>
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
                                                                                              </asp:GridView>
            
            </div>
        </form>
    </body>
</html>