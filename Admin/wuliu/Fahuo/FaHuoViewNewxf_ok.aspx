<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoViewNewxf_ok.aspx.cs" Inherits="Admin_FaHuoViewNewxf_ok" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../../../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"   DataKeyNames="fhkey"
                     OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="箱标序号">
                            <ItemTemplate>
                                <asp:Label ID="Label" runat="server" Text='<%# labelcode%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货经销商">
                            <ItemTemplate>
                                <asp:Label ID="Labelawtype" runat="server" Text='<%# bll.GetList( int.Parse(Eval("agentid").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="收货时间">
                            <ItemTemplate>
                                <asp:Label ID="Labeldt" runat="server" Text='<%# Bind("tm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                
                               <asp:TemplateField HeaderText="状态">
                            <ItemTemplate>
                                <asp:Label ID="labelgegnxin" runat="server" Text='<%# (Eval("tm").ToString().Equals("")?"未确认":"已确认") %>'></asp:Label>
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
        </div>
        <script src="../../../js/jquery-1.7.1.js"></script>
        <script src="../../../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../../../include/js/jquery.easyui.min.js"></script>
        <asp:HiddenField ID="hf_acid" runat="server" />
        <asp:HiddenField runat="server" ID="hf_acname" />
    </form>
</body>
</html>
