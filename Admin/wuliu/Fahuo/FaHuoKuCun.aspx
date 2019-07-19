<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoKuCun.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoKuCun" %> 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
        <link href="../../../include/MasterPage.css" rel="stylesheet" />
          <link href="../../../include/easyui.css" rel="stylesheet" />
        <style type="text/css">
            .auto-style1 {
                height: 33px;
                width: 100%;
            }

            .auto-style2 {
                height: 240px;
                width: 100%;
            }

            .auto-style3 { width: 38px; }

            .grid { text-align: center; }
           
            .ComboBox_default {
                height: 25px !important;
                line-height: 25px !important; 
            }
             .ComboBox_default .arrow_FW {
                 height: 35px !important;
                 line-height: 25px !important;
                 -ms-background-size: 100% 100% !important;
                 background-size: 100% 100% !important;
                    border: 1px  solid lightgrey !important;
                  margin-bottom: 1px !important;
             }
             .ComboBox_default .text_FW {
                 height: 35px !important;
                 line-height: 25px !important;
                    border: 1px   solid lightgrey !important;
             }
            .ComboBox_default .popup_FW li {
                line-height: 20px !important;
                height: 20px !important;
            } 
        </style>

    </head>
    <body>
        <form id="form1" runat="server">
           <div class="div_WholePage">  
                   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                   </asp:ScriptManager>
                   <div class="topdiv">
                       <div class="topitem">
                           <span>起始时间:</span>
                       </div>
                       <div class="topitem">
                           <asp:TextBox ID="TextBox_RukuDateBegin" runat="server" class="inputsearch"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_RukuDateBegin" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                       </div>
                       <div class="topitem">
                           <span>至</span>
                       </div>
                       <div class="topitem">
                           <asp:TextBox ID="TextBox_RukuDateEnd" runat="server" class="inputsearch"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_RukuDateEnd" Format="yyyy-MM-dd" runat="server"></cc1:CalendarExtender>
                       </div>
                     <div class="topitem">
                           <span>代理商:</span></div>
              <div class="topitem  ">
                                <asp:DropDownList ID="ComboBox_DaiLiShangID" runat="server" AppendDataBoundItems="true" DataTextField="CompName" DataValueField="CompID">
                                    <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                         </div>
                            <%--<td nowrap="nowrap">产 品:</td>
                        <td nowrap="nowrap" >
                            <cc2:ComboBox ID="ComboBox_ProInfo" runat="server" AppendDataBoundItems="true" DataTextField="Products_Name" DataValueField="Infor_ID">
                                <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                            </cc2:ComboBox>
                        </td>
                        <td nowrap="nowrap">货仓:</td>
                        <td nowrap="nowrap">
                            <cc2:ComboBox ID="ComboBox_StoreHouse" runat="server" AppendDataBoundItems="true" DataTextField="StoreHouseName" DataValueField="STID">
                                <asp:ListItem Text="全部..." Value="0" Selected="True"></asp:ListItem>
                            </cc2:ComboBox>
                        </td>--%>
                                   <div class="topitem">
                                <asp:Button ID="Button_Search" runat="server" Text="查询全部经销商" OnClick="Button_Search_Click" CssClass="btn btn-warning btnyd" Width="150px" /></div>
                             <div class="topitem">
                                <asp:Button ID="Button2" runat="server" Text="单个经销商发货查询" OnClick="Button2_Click" CssClass="btn btn-warning btnyd"  /></div>
                      <div class="topitem">
                          <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" Text="导出EXCEL" OnClick="Button1_Click"  /></div>
                   
                </div>
                <table style="width: 100%">
                    <tr>
                        <td >
                            <asp:Label ID="Label1" runat="server" Text="发货产品列表:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" valign="top" >
                            <asp:GridView ID="GridView_RukuInfo" Width="100%" runat="server" CssClass="grid"  CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="GridView_RukuInfo_RowDataBound">
                                <Columns>
                                    <%-- <asp:BoundField HeaderText="代理商ID" DataField="AgentID" />
                                <%-- <asp:TemplateField HeaderText="批次">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>合计：</FooterTemplate>
                            </asp:TemplateField>--%>
                                    <%-- <asp:BoundField DataField="FHDate" HeaderText="发货日期" />--%>
                                    <asp:TemplateField HeaderText="发货商" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="labelCompID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("CompID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="发往经销商" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="labelAgentID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="产品" ItemStyle-Width="160px">
                                        <ItemTemplate>
                                            <asp:Label ID="labelProduct" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="发货日期">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelFHR" runat="server" Text='<%#Convert.ToDateTime(Eval("FHDate").ToString()).ToString("yyyy-MM-dd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderText="总量">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_jianshu" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            合计：
                                            <asp:Label ID="Label_jianshu_ft" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="下级经销商已发货">

                                        <ItemTemplate>
                                            <asp:Label ID="LabYiFaHuo" runat="server" Text='<%# comm.getDailishangkucun(Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="库存剩余">

                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# kcsy(int.Parse(Eval("XiangNumber").ToString()), Eval("AgentID").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="详细">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# JiageLinkString(Eval("AgentID").ToString()) %>' Text='详细'></asp:HyperLink>
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
                        </td>
                    </tr> 
                    <tr>
                        <td >
                            <asp:Label ID="Label2" runat="server" Text="产品列表:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td nowrap="noWrap" valign="top" >
                            <asp:GridView ID="GridViewSH" Width="100%" runat="server" CellPadding="2" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" PageSize="20" ShowFooter="True" GridLines="Vertical" Font-Size="Medium" AutoGenerateColumns="False" OnRowDataBound="GridViewSH_RukuInfo_RowDataBound">
                                <Columns>
                                    <%-- <asp:TemplateField HeaderText="批次">                               
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("FHPiCi") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>合计：</FooterTemplate>
                            </asp:TemplateField>--%>
                                    <%-- <asp:BoundField DataField="FHDate" HeaderText="发货日期" />--%>
                                    <asp:TemplateField HeaderText="发货商">
                                        <ItemTemplate>
                                            <asp:Label ID="FHsID" runat="server" Text='<%# bagent.GetList(int.Parse(Eval("CompID").ToString())).CompName %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                               
                                    <asp:BoundField DataField="Products_Name" HeaderText="产品" />
                                    <asp:BoundField DataField="StoreHouseName" HeaderText="库房" />
                                    <asp:TemplateField HeaderText="发货人">
                                        <ItemTemplate>
                                            <asp:Label ID="LabelFHS" runat="server" Text='<%# Bind("FHUserID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="总量">
                                        <ItemTemplate>
                                            <asp:Label ID="LFhNum" runat="server" Text='<%# Bind("XiangNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            合计：
                                            <asp:Label ID="LallNum" runat="server" ></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="我的发货量">

                                        <ItemTemplate>
                                            <asp:Label ID="LMeFaHuo" runat="server" Text='<%# comm.getDailishangkucun(Eval("AgentID").ToString(), TextBox_RukuDateBegin.Text, TextBox_RukuDateEnd.Text) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="库存剩余">

                                        <ItemTemplate>
                                            <asp:Label ID="LKC" runat="server" Text='<%# kcsy(int.Parse(Eval("XiangNumber").ToString()), Eval("AgentID").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="详细">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hpmlink" runat="server" NavigateUrl='<%# JiageLinkString(Eval("AgentID").ToString()) %>' Text='详细'></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

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
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </body>
</html>