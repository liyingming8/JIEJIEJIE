<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_LuoDiYeConfig.aspx.cs" Inherits="Admin_TJ_LuoDiYeConfig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>落地页面</title>
       <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
   
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="div_WholePage">
                <div class="topdiv">
                    <div class="topitem"><input type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_LuoDiYeConfigAddEdit.aspx?cmd=add', 400, 300, '落地页面配置')" /></div>
                    <div class="topitem"><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="AlertContents">警告信息</asp:ListItem>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList></div>
                    <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                    <div><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
                </div>  
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　  AutoGenerateColumns="False" DataKeyNames="LDYCFGID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="LDYCFGID" DataNavigateUrlFormatString="TJ_LuoDiYeConfigAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="所属公司">
                            <ItemTemplate>
                                <asp:Label ID="LabelCompID" runat="server" Text='<%# bcompany.GetList(int.Parse(Eval("CompID").ToString())).CompName %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品">
                            <ItemTemplate>
                                <asp:Label ID="LabelWLProID" runat="server" Text='<%# GetProductName(Eval("WLProID").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="限查次数">
                            <ItemTemplate>
                                <asp:Label ID="LabelLimitedCheckNum" runat="server" Text='<%# Bind("LimitedCheckNum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="警告信息">
                            <ItemTemplate>
                                <asp:Label ID="LabelAlertContents" runat="server" Text='<%# Bind("AlertContents") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="顶部广告">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="checkbox_ShowTopAd" Checked='<%#Eval("ShowTopAd") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="身份验证">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox_ShowSuYuan" runat="server" Checked='<%#Eval("ShowSuYuan") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="功能模块">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox_ShowModules" runat="server" Checked='<%# Eval("ShowModules") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="溯源信息">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox_ShowTraceInfo" runat="server" Checked='<%# Eval("ShowTraceInfo") %>' />                        
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <ItemTemplate>
                                <asp:Label ID="Label_Remarks" runat="server" Text='<%# Eval("Remarks") %>' />                           
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
            <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>
    </body>
</html>