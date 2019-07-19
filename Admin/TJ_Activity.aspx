<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity.aspx.cs" Inherits="Admin_TJ_Activity" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_WholePage">
            <div class="topdiv">
                <div class="topitem">
                    <input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_ActivityAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,470,'活动信息')" /></div>
                <div class="topitem"><span></span></div>
                <div class="topitem">
                    <asp:DropDownList ID="DDLField" runat="server"> 
                        <asp:ListItem Value="AName">活动名称</asp:ListItem>  
                        <asp:ListItem Value="Remarks">备注</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="topitem"><span>包含</span></div>
                <div class="topitem">
                    <input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" /></div>
            </div>
            <div style="overflow-x: auto;">
                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="id,AName,FaceTo"
                    OnRowDeleting="GridView1_RowDeleting" OnRowDataBound="GridView1_RowDataBound" CssClass="GridViewStyle">
                    <Columns>
                        <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Label ID="LabelIndex" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="活动名称">
                            <ItemTemplate>
                                <asp:Label ID="LabelAName" runat="server" Text='<%# Bind("AName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖励策略">
                            <ItemTemplate>
                                <asp:Label ID="LabelASID" runat="server" Text='<%# Commfrank.GetActivityStrategyDiscription(int.Parse(Eval("ASID").ToString()),true)  %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="活动对象">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="labelfaceto"  Text='<%#ReturnFaceTo(Eval("FaceTo").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="开始时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelSTM" runat="server" Text='<%# Convert.ToDateTime(Eval("STM")).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelETM" runat="server" Text='<%# Convert.ToDateTime(Eval("ETM")).ToString("yyyy-MM-dd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="码类型">
                            <ItemTemplate>
                                <asp:Label ID="LabelObCodeType" runat="server" Text='<%# ReturnCodeType(Eval("ObCodeType").ToString(),Eval("YZM")) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖项设置">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyperlinkJiangxiang" runat="server" ForeColor="#FF3300" Font-Bold="True">奖项设置</asp:HyperLink> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="范围限定">
                            <ItemTemplate> 
                                <asp:HyperLink ID="hlinkprod" ForeColor="#009900" runat="server" >产品</asp:HyperLink><asp:Label ID="lbprodnum" ForeColor="red" runat="server" Text='<%# (Eval("prodnum").ToString().Equals("0")?"(不限)":"("+Eval("prodnum")+")") %>'></asp:Label>| 
                                <asp:HyperLink ID="hlinkagent" ForeColor="#009900" runat="server">经销商</asp:HyperLink><asp:Label ID="lbagentnum" runat="server" ForeColor="red" Text='<%# (Eval("agentnum").ToString().Equals("0")?"(不限)":"("+Eval("agentnum")+")") %>'></asp:Label>|   
                                <asp:HyperLink ID="hlinkterminal" ForeColor="#009900" runat="server">终端店</asp:HyperLink><asp:Label ID="lbterminalnum" runat="server" ForeColor="red" Text='<%# (Eval("terminalnum").ToString().Equals("0")?"(不限)":"("+Eval("terminalnum")+")") %>'></asp:Label>|
                                 <asp:HyperLink ID="hlinkother" ForeColor="#009900" runat="server">其他</asp:HyperLink> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>   
                        <asp:TemplateField HeaderText="更新人">
                            <ItemTemplate>
                                <asp:Label ID="LabelCreateID" runat="server" Text='<%# BtjUser.GetList(int.Parse(Eval("CreateID").ToString())).LoginName%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="更新时间">
                            <ItemTemplate>
                                <asp:Label ID="LabelCreateDate" runat="server" Text='<%# Bind("CreateDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- 
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" />
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
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
    </form>
</body>
</html>
