<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanGrain.aspx.cs" Inherits="Admin_TB_SuYuanGrain" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv"> 
                    <div class="topitem">
                        <input type="button" value="入库" class="btn btn-warning btnyd" onclick="openWinCenter('TB_SuYuanGrainAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 800, 600, '原粮入库')" />
                    </div>
                    <div class="topitem">
                        <asp:DropDownList ID="DDLField" runat="server">
                            <asp:ListItem Value="GainName">原粮名称</asp:ListItem>
                            <asp:ListItem Value="PiCI">入库批次</asp:ListItem>
                            <asp:ListItem Value="Remarks">备注</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                <div class="topitem">
                    <input id="inputSearchKeyword" placeholder="请输入查找内容" type="text" runat="server" class="inputsearch" />
                    </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div> 
            </div> 
            <div style="overflow-x:auto"><asp:GridView ID="GridView1" runat="server" Width="100%" EnableViewState="False" AutoGenerateColumns="False" DataKeyNames="ID"
                OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="原粮类别">
                        <ItemTemplate>
                            <asp:Label ID="LabelGainName" runat="server" Text='<%# Bind("GainName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label ID="LabelCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <ItemTemplate>
                            <asp:Label ID="LabelUNCID" runat="server" Text='<%# BaseClass.GetList(int.Parse(Eval("UNCID").ToString())).CName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="入库批次">
                        <ItemTemplate>
                            <asp:Label ID="LabelPiCI" runat="server" Text='<%# Bind("PiCI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库房">
                        <ItemTemplate>
                            <asp:Label ID="LabelKuFang" runat="server" Text='<%# BtbSuYuanKuFang.GetList(int.Parse(Eval("KFID").ToString())).KuFang  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="日期">
                        <ItemTemplate>
                            <asp:Label ID="LabelRuKuTime" runat="server" Text='<%# Convert.ToDateTime(Eval("RuKuTime")).ToString("yyyy-MM-dd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供应商">
                        <ItemTemplate>
                            <asp:Label ID="LabelGYSID" runat="server" Text='<%# BtbSuYuanGong.GetList(int.Parse(Eval("GYSID").ToString())).GYSName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="质检员">
                        <ItemTemplate>
                            <asp:Label ID="LabelZhiJianID" runat="server" Text='<%# BtbSuYuanZhi.GetList(int.Parse(Eval("ZhiJianID").ToString())).ZhiJianName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="检测机构">
                        <ItemTemplate>
                            <asp:Label ID="LabelJCDWID" runat="server" Text='<%# BtbSuYuanJianCeJiGou.GetList(int.Parse(Eval("JCDWID").ToString())).JGName %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="质检报告">
                        <ItemTemplate>
                            <img id="Imageurl_small" runat="server" src='<%# Eval("Imageurl_small").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True">
                        <ItemStyle  HorizontalAlign="Center" Width="50px" />
                    </asp:CommandField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView></div>
            <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle" PageSize="15" OnPageChanging="AspNetPager1_PageChanging"  NumericButtonCount="5"  CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页" ></webdiyer:AspNetPager>
        </div>
        <script src="../include/js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script> 
    </form>
</body>
</html>
