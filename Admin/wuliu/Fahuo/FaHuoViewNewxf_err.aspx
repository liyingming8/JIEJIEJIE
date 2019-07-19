<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoViewNewxf_err.aspx.cs" Inherits="Admin_FaHuoViewNewxf_err" %>

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
                <div class="topitem">
                    <%--<input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_Activity_PrizesAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>&acid=<%=Sc.EncryptQueryString(hf_acid.Value)%>', 600,420,'活动奖项')" />--%>
                </div>
                <%--<div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server">
                        <asp:ListItem Value="remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" />
                </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>--%>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"   DataKeyNames="to_fhkey"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle" ShowFooter="True">
                    <Columns>

                        <asp:TemplateField HeaderText="收货经销商">
                            <ItemTemplate>
                                <asp:Label ID="Labelawtype" runat="server" Text='<%# bll.GetList( int.Parse(Eval("agentid").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="原发货经销商">
                            <ItemTemplate>
                                <asp:Label ID="lab_activity_prize_discription" runat="server" Text='<%# bll.GetList( int.Parse(Eval("to_agentid").ToString())).CompName  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="收货数量">
                            <ItemTemplate>
                                <asp:Label ID="Labelawid" runat="server" Text='<%# Bind("数量") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货时间">
                            <ItemTemplate>
                                <asp:Label ID="Labeldt" runat="server" Text='<%# Bind("dt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="收货确认地址">
                            <ItemTemplate>
                                <asp:Label ID="Labeldz" runat="server" Text='<%# agentaddress(Eval("receipt_batch_id").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                               <asp:TemplateField HeaderText="处理结果">
                            <ItemTemplate>
                                <asp:Label ID="labelgegnxin" runat="server" Text='<%# (Eval("tm_handled").ToString().Equals("")?"未处理":"已处理") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" DeleteText="更新">
                            <ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" />
                        </asp:CommandField>
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
