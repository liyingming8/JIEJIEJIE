<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DepartMentForAuthor.aspx.cs" Inherits="Admin_TJ_DepartMentForAuthor" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="overflow-x: auto;">
                        <div><span><asp:Literal ID="author_agent" runat="server"></asp:Literal></span></div>
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                            OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                            <Columns>
                                <asp:TemplateField HeaderText="序号">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hf_id" Value='<%# Bind("id") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="父级">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelparentid" runat="server" Text='<%# Bind("parentdepartment") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="单位部门">
                                    <ItemTemplate>
                                        <asp:Label ID="Labeldepartment" runat="server" Text='<%# Bind("department") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="负责人">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelleadername" runat="server" Text='<%# Bind("leadername") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="指定">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="ckb_select" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                        <div style="text-align: center; padding: 1rem;">
                            <input type="button" id="btn_confirm" runat="server" class="btn btn-warning btnyd" value="确定" OnServerClick="btn_confirm_Click"   />
                        </div>
                    </div>
                    <input id="hd_agentid" runat="server" type="hidden" />
                    <input id="hd_authordepartid" runat="server" type="hidden"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script type="text/javascript">
            function select(index) {
                var eleid = 'GridView1_ctl' + (parseInt(index) + 2 + 100).toString().substring(1) + '_',
                    elename = eleid + "ckb_select";
                if ((document.getElementById(elename)).checked) {
                    (document.getElementById(elename)).checked = false;

                } else {
                    (document.getElementById(elename)).checked = true;
                }
            }
        </script>
    </form>
</body>
</html>
