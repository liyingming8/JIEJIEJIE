<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_QuerLabelCodeMingXi.aspx.cs" Inherits="Admin_TJ_QuerLabelCodeMingXi" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <style type="text/css">
            .auto-style1 {
                BACKGROUND: #e6eff8;
                LINE-HEIGHT: 120%;
                width: 177px;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server">

                                             <asp:ListItem Value="LabelCode">标签序号</asp:ListItem>

                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" placeholder="请输入查找内容" runat="server" class="inputtext" /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td  ></td><td></td></tr></table>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <%-- <asp:TemplateField>
            <ItemTemplate>
                 <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# XiangXiLinkString(Eval("ID").ToString()) %>' Text="详细"></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>--%>
        
               
                        <asp:TemplateField HeaderText="产品名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelProID" runat="server" Text='<%# bpro.GetList(int.Parse(Eval("ProID").ToString())).Products_Name %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查询时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelSaoMaTime" runat="server" Text='<%# Bind("SaoMaTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="代理商">
                            <ItemTemplate>
                                <asp:Label ID="LabelagentID" runat="server"  Text='<%# bagent.GetList(int.Parse(Eval("AgentID").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="查询地址">
                            <ItemTemplate>
                                <asp:Label ID="LabelSaoMaAddress" runat="server" Text='<%# Bind("SaoMaAddress") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标签序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelLabelCode" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="扫码工具">
                            <ItemTemplate>
                                <asp:Label ID="LabelSaoMaType" runat="server" Text='<%# Bind("SaoMaType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
               
               
                        <asp:TemplateField HeaderText="发货批次">
                            <ItemTemplate>
                                <asp:Label ID="LabelFHPICI" runat="server" Text='<%# Bind("FHPICI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
               
              
               

                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>