<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoOutFile.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoOutFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
        <link href="../../../include/MasterPage.css" rel="stylesheet" /> 
         <link href="../../../include/easyui.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="div_WholePage">
                <div >
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                    </asp:ScriptManager>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="提示:关键字以空格隔开,条件为空则返回全部数据!"></asp:Label>
                </div>
   
                <table style="padding: 1px; margin: 0px; border: 1px solid #C0C0C0; border-collapse: collapse"> 
                    <tr>
                        <td style="padding: 5px; border-bottom-style: solid; border-width: 1px; border-color: #C0C0C0; border-right-style: solid; " >
                         产品: <asp:TextBox ID="inputSearchProduct" runat="server" CssClass=" inputsearch" ></asp:TextBox>
                           <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="检 索" CssClass=" btn btn-warning btnyd" /></td>
                        <td style="padding: 5px; border-bottom-style: solid; border-width: 1px; border-color: #C0C0C0; border-right-style: solid;" >
                            代理商:
                        <asp:TextBox ID="TextBox_AgentFilter" runat="server" CssClass="inputsearch"></asp:TextBox>
                           <asp:Button ID="Button2" runat="server" Text="检 索" OnClick="Button2_Click"  CssClass=" btn btn-warning btnyd"/>
                        </td>
                        <td style="padding: 5px; border-bottom-style: solid; border-width: 1px; border-color: #C0C0C0" >
                            货仓: <asp:TextBox ID="TextBox_StoreHouseFilter" runat="server" CssClass="inputsearch"></asp:TextBox>
                           <asp:Button ID="Button3" runat="server" Text="检 索" OnClick="Button3_Click" CssClass=" btn btn-warning btnyd"/></td>
                    </tr>
                    <tr>
                        <td style="padding: 5px; border-width: 1px; border-color: #C0C0C0; vertical-align: top; text-align: left; border-right-style: solid;">
                            <asp:GridView ID="GridView_TbProductsInfor" runat="server" AutoGenerateColumns="False"
                                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="3" OnPageIndexChanging="GridView_TbProductsInfor_PageIndexChanging" ShowFooter="True">
                               <Columns>
                                    <asp:BoundField DataField="Infor_ID"  HeaderText="产品编码" />
                                    <asp:BoundField DataField="Products_Name"  HeaderText="产品名称" /> 
                                    <asp:TemplateField HeaderText="香型"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# (Eval("ProductXiangXing").Equals(null) ? "" : pxiangxing.GetList(Convert.ToInt32(Eval("ProductXiangXing").ToString())).XiangXing) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="酒精度">  
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server"  Text='<%# (Eval("ProductJiuJingDu").Equals(null) ? "" : pjiujingdu.GetList(Convert.ToInt32(Eval("ProductJiuJingDu").ToString())).JiuJingDuShu) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="净含量"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# (Eval("ProductJingHanLiang").Equals(null) ? "" : pjinghanliang.GetList(Convert.ToInt32(Eval("ProductJingHanLiang").ToString())).JingHanLiang) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="包装规格"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("Products_Standards") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate><asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="导出(产品)" CssClass=" btn btn-warning btnyd" /></FooterTemplate>
                                    </asp:TemplateField> 
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    尚未找到相关信息!
                                </EmptyDataTemplate>
                          <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" />
                            </asp:GridView></div>
                        </td>
                        <td style="padding: 5px; border-width: 1px; border-color: #C0C0C0; vertical-align: top; text-align: left; border-right-style: solid;">
                            <asp:GridView ID="GridView_AgentsInfor" runat="server" AutoGenerateColumns="False"
                                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left"  OnPageIndexChanging="GridView_AgentsInfor_PageIndexChanging" ShowFooter="True">
                           <Columns> 
                                    <asp:BoundField DataField="CompID"  HeaderText="编码" />
                                    <asp:BoundField DataField="CompName"  HeaderText="代理商" /> 
                                    <asp:TemplateField HeaderText="销售区域"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("AllowAreaInfo") %>'></asp:Label> 
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="导出(代理)" CssClass=" btn btn-warning btnyd" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    尚未找到相关信息!
                                </EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" />
                            </asp:GridView></div>
                        </td>
                        <td style="padding: 5px; vertical-align: top; text-align: left;">
                            <asp:GridView ID="GridView_StoreHouse" runat="server" AutoGenerateColumns="False"
                                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CaptionAlign="Left" CellPadding="3" OnPageIndexChanging="GridView_StoreHouse_PageIndexChanging" ShowFooter="True">
                               <Columns> 
                                    <asp:BoundField DataField="STID"  HeaderText="货仓编码" />
                                    <asp:TemplateField HeaderText="货仓名称"> 
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("StoreHouseName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate><asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="导出(货仓)" CssClass=" btn btn-warning btnyd" /></FooterTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    尚未找到相关信息!
                                </EmptyDataTemplate>
                           <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" />
                            </asp:GridView></div> 
                        </td>
                    </tr>
                    </table> 
            </div>
        </form>
    </body>
</html>