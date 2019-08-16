<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_do_Holiday.aspx.cs" Inherits="Admin_TJ_do_Holiday" %>

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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="div_WholePage">
                    <div class="container topdiv">
                        <div class="topitem">
                            <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_HolidayAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'节日活动')" />
                        </div>
                        <div class="topitem"><span></span></div>
                        <div class="topitem"><span>包含</span></div>
                        <div class="topitem">
                            <input id="inputSearchKeyword" type="text" runat="server" style="width: 100px" class="inputsearch" />
                        </div>
                        <div class="topitem">
                            <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                        </div>
                    </div>
                    <div style="overflow-x: auto; padding-bottom: 50px;">
                        <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
                            OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                            <Columns>
                                <asp:TemplateField HeaderText="编辑">
                                    <ItemTemplate>
                                        <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="节日">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hf_gmid" Value='<%# Eval("gameid").ToString() %>' />
                                        <asp:HiddenField runat="server" ID="hd_hdid" Value='<%# Eval("id").ToString() %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="开放">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="ckb_isopen" AutoPostBack="True" OnCheckedChanged="ckb_isopen_OnCheckedChanged" Checked='<%# Convert.ToBoolean(Eval("isopen")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="广告图">
                                    <ItemTemplate>
                                        <img id="Labelimg" src='<%# Eval("img") %>' height="50" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="主题">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelsubtitle" runat="server" Text='<%# Bind("subtitle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="内容">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelcontents" runat="server" Text='<%# Bind("contents") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                           <%--     <asp:CommandField ShowDeleteButton="True">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:CommandField>--%>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                        </asp:GridView>
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle" PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
                    </div>
                </div>
                <input type="hidden" id="input_bhid" runat="server" />
                <input type="hidden" id="hd_gmid" />
                <input type="hidden" id="hdstop" value="0" />
            </ContentTemplate>
        </asp:UpdatePanel> 
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <script src="js/frankprocess.js"></script> 
    </form>
</body>
</html>
