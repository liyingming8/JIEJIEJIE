<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoOutFilejg.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoOutFilejg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
        <link href="../../include/MasterPage.css" rel="stylesheet" />
        <style type="text/css">
            .auto-style2 { height: 30px; }

            .auto-style3 { height: 24px; }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>    
                <div style="background-color: white; border-radius: 6px; border: 1px solid gray; padding: 5px 5px;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
                    </asp:ScriptManager>
                </div>
   
                <table width="100%">
                    <tr>
                        <td colspan="3" >
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="提示:关键字以空格隔开,条件为空则返回全部数据!"></asp:Label></td>
                    </tr>
                    <tr>
                        <td >
                            产品检索:</td>
                        <td >
              
                            <asp:TextBox ID="inputSearchProduct" runat="server" Width="200px"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="检索" /></td>
                        <td >
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 20px">
                            <asp:GridView ID="GridView_TbProductsInfor" Width="100%" runat="server" AutoGenerateColumns="False"
                                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                          Caption="产品信息" CaptionAlign="Left" CellPadding="3" OnPageIndexChanging="GridView_TbProductsInfor_PageIndexChanging">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="Infor_ID"  HeaderText="产品编码" />
                                    <asp:BoundField DataField="Products_Name"  HeaderText="产品名称" />
                                    <%--<asp:TemplateField HeaderText="产品编码">
              
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Product_Code") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="产品名称">
               
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Products_Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
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
                                    </asp:TemplateField>
            
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    尚未找到相关信息!
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView></div>
                        </td>
                        <td style="height: 20px">
                            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="导出(产品)" /></td>
                    </tr>
                    <tr>
                        <td>
                            代理商检索:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_AgentFilter" runat="server" Width="200px"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="检索" OnClick="Button2_Click" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView_AgentsInfor" Width="100%" runat="server" AutoGenerateColumns="False"
                                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                          Caption="代理商信息" CaptionAlign="Left" CellPadding="3" OnPageIndexChanging="GridView_AgentsInfor_PageIndexChanging">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <%--            <asp:BoundField DataField="Province" HeaderText="省份" />
            <asp:BoundField DataField="City" HeaderText="城市" />
--%>            
           
                                    <asp:BoundField DataField="CompID"  HeaderText="编码" />
                                    <asp:BoundField DataField="CompName"  HeaderText="代理商" />
           
                                    <asp:TemplateField HeaderText="销售区域">
               
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("AllowAreaInfo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    尚未找到相关信息!
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView></div>
                        </td>
                        <td>
                            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="导出(代理)" /></td>
                    </tr>
                    <tr>
                        <td>
                            货仓检索:</td>
                        <td>
                            <asp:TextBox ID="TextBox_StoreHouseFilter" runat="server" Width="200px"></asp:TextBox>
                            <asp:Button ID="Button3" runat="server" Text="检索" OnClick="Button3_Click" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView_StoreHouse" runat="server" AutoGenerateColumns="False"
                                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                          Caption="货仓信息" CaptionAlign="Left" CellPadding="3" OnPageIndexChanging="GridView_StoreHouse_PageIndexChanging">
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <RowStyle ForeColor="#000066" />
                                <Columns>
            
                                    <asp:BoundField DataField="STID"  HeaderText="货仓编码" />
                                    <asp:BoundField DataField="StoreHouseName"  HeaderText="货仓名称" />
            
                                </Columns>
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <EmptyDataTemplate>
                                    尚未找到相关信息!
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView></div>
                        </td>
                        <td>
                            <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="导出(货仓)" /></td>
                    </tr>
                </table>
                <br />


            </div>
        </form>
    </body>
</html>