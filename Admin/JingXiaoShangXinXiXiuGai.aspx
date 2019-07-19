<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JingXiaoShangXinXiXiuGai.aspx.cs" Inherits="Admin_JingXiaoShangXinXiXiuGai" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div><span>经销商名称:</span><input type="text" runat="server" id="txt_jingxiaoshang"/><asp:Button runat="server" ID="btn_chaxun" Text="查询" OnClick="btn_chaxun_Click"/><asp:CheckBox runat="server" ID="ckb_all" Text="全部发货详细"/></div>
        <div style="overflow-x: auto;">
            <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="CompID,CompName" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="ckb_save" Text="保留"/>
                            <asp:HiddenField runat="server" ID="hd_agentid" Value='<%#Eval("CompID") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="ckb_contain" Checked="True" Text="包含"/> 
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="经销商">
                        <ItemTemplate>
                            <asp:Label ID="labeljingxiaoshang" runat="server" Text='<%# Eval("CompName").ToString() %>'></asp:Label>
                        </ItemTemplate> 
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="注册时间">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lab_zhuceshijian" Text='<%# Eval("RegisterDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发货数量">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lab_fhsl" Text='<%#FaHuoQingKuang(Eval("CompID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="终端数量">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lab_zdsl" Text='<%#XiaJiShuLiang(Eval("CompID").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView>  
        </div>
        <asp:Button ID="btn_do" runat="server" OnClick="btn_do_Click" Text="处理" />
    </form>
</body>
</html>
