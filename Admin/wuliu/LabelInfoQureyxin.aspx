<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelInfoQureyxin.aspx.cs" Inherits="Admin_wuliu_LabelInfoQureyxin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title>标签情况查询</title>
    <link href="../../include/MasterPage.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">标签号码</div>
                <div class="topitem">
                    <input  id="TextBox_Label" type="text" runat="server" max="16" class="inputsearch"/></div>
                <div class="topitem">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="单枚查询" /></div>
            </div>
            <div style="overflow-x: auto">
                <table>
                    <tr>
                        <td>瓶箱关系</td>
                        <td>替换关系</td>
                        <td>套标对应关系</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView_QueryResult" runat="server" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" CellPadding="2" Font-Size="Medium" GridLines="Vertical" PageSize="20" AutoGenerateColumns="False" CellSpacing="2">
                                <RowStyle Height="20px" Wrap="False" />
                                <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="瓶标" HeaderText="瓶标" />
                                    <asp:BoundField DataField="箱标1" HeaderText="箱标1" />
                                </Columns>
                                <EmptyDataTemplate>
                                    尚未查询到满足条件的入库记录!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#004A66" Font-Size="13px" ForeColor="White" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="GridView1" EnableViewState="False" runat="server" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" CellPadding="2" Font-Size="Medium" GridLines="Vertical" PageSize="20">
                                <RowStyle Height="20px" Wrap="False" />
                                <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                                <EmptyDataTemplate>
                                    尚未查询到满足条件的入库记录!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#004A66" Font-Size="13px" ForeColor="White" Height="30px"
                                    HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:GridView ID="GridView2" runat="server" Width="100%" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" CellPadding="2" Font-Size="Medium" GridLines="Vertical" PageSize="20" AutoGenerateColumns="False" CellSpacing="2">
                                <RowStyle Height="20px" Wrap="False" />
                                <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="LabelCodeOld" HeaderText="替换前" />
                                    <asp:BoundField DataField="LabelCodeNew" HeaderText="替换后" />
                                    <asp:TemplateField HeaderText="替换类型">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# (Eval("Flag").ToString().Trim()=="1")?"瓶标":"箱标" %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无替换记录!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#004A66" Font-Size="13px" ForeColor="White" Height="30px" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                                <FooterStyle BackColor="#157fcc" ForeColor="White" Font-Bold="true" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="overflow-x: auto;">
                <asp:Label ID="Label_fahuoInfo" runat="server" Font-Size="13px" Font-Bold="true" Text="发货信息:" Visible="False"></asp:Label><br />
                <asp:GridView ID="GridView_FaHuoXinXi" Width="100%" runat="server" BorderColor="Silver" BorderStyle="Outset" BorderWidth="1px" CaptionAlign="Left" CellPadding="2" Font-Size="Medium" GridLines="Vertical" PageSize="20" AutoGenerateColumns="False">
                    <RowStyle Height="20px" Wrap="False" />
                    <PagerStyle BorderColor="Gray" BorderStyle="Double" BorderWidth="1px" />
                    <Columns>
                        <asp:BoundField DataField="BoxLabel01" HeaderText="箱标号码" />
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelPRODU" runat="server" Text='<%# ReturnProductNameByID(comm.getproIDbyFhKey((Eval("FHKey").ToString()),Eval("FromAgentID").ToString()) )%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FHDate" HeaderText="时间" />
                        <asp:TemplateField HeaderText="从">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ReturnAgentNameAndCity(Eval("FromAgentID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="库">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# ReturnStoreHouseNameByID(Eval("FromStorehouseID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="LabelFHType" runat="server" Text='<%# com.FHTypeName(Eval("FHType").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="至">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# ReturnAgentNameAndCity(Eval("ToAgentID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="操作员">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# buser.GetList(Convert.ToInt32(Eval("UserID").ToString())).LoginName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="批次">
                            <ItemTemplate>
                                <asp:Label ID="Labelpici" runat="server" Text='<%# (comm.getfhinfobyFhKey((Eval("FHKey").ToString()),Eval("FromAgentID").ToString())).Rows[0]["FHPiCi"].ToString()%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="该批次发货数量">
                            <ItemTemplate>
                                <asp:Label ID="Labelpicinum" runat="server" Text='<%# (comm.getfhinfobyFhKey((Eval("FHKey").ToString()),Eval("FromAgentID").ToString())).Rows[0]["XiangNumber"].ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        尚未查询到满足条件的发货记录!
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#004A66" Font-Size="13px" ForeColor="White" Height="30px"
                        HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
                </asp:GridView>
            </div> 
        </div>
        <asp:HiddenField ID="HiddenField1" runat="server" /> 
    </form>
</body>
</html>
