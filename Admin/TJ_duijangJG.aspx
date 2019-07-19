<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_duijangJG.aspx.cs" Inherits="TJ_duijangJG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
    <title></title>
     
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" runat="server">
             <div class="div_WholePage"> 
                <div class="topdiv">
                    <div class="topitem"><span>手机号码：</span></div>
                    <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容" class="inputsearch" /></div>
                    <div class="topitem"><span>中奖码：</span></div>
                    <div class="topitem"><input id="inputSearchKeyword0" runat="server" class="inputsearch" type="text" /></div>
                    <div class="topitem"><asp:Button ID="BtnSearch2" runat="server" Text="查询" CssClass="btn btn-default btnyd" OnClick="BtnSearch2_Click" /></div>
                    <div class="topitem"><asp:Button ID="Button1" runat="server" Text="兑奖" CssClass="btn btn-warning btnyd"  OnClick="Button1_Click1"/></div>
                </div>  
                  <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gv" runat="server" AllowPaging="True"　 AutoGenerateColumns="False"> 
                    <Columns> 
                        <asp:TemplateField HeaderText="中奖标签号码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZjNumber" runat="server" Text='<%# Bind("LabelCode") %>'   MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelZjNumber" runat="server" Text='<%# Bind("LabelCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中奖时间">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZjTime" runat="server" Text='<%# Bind("LQDateTime") %>' MaxLength="16"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelZjTime" runat="server" Text='<%# Bind("LQDateTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖项名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJxName" runat="server" Text='<%# Bind("JXName") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelJxName" runat="server" Text='<%# Bind("JXName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中奖手机号码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZjPhone" runat="server" Text='<%# Bind("Phone") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelZjPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="兑换时间">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDhTime" runat="server"  Text='<%# Bind("DhTime") %>' MaxLength="16"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDhTime" runat="server" Text='<%# Bind("DhTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否领奖">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDjFlag" runat="server" Text='<%# Bind("DJflag") %>' MaxLength="20"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelDjFlag" runat="server" Text='<%# Eval("DJflag").ToString() == "1" ? "已领奖" : "未领奖" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="领奖时间">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLjTime" runat="server" Text='<%# Bind("DHtime") %>' MaxLength="16"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelLjTime" runat="server" Text='<%# Bind("DHtime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="中奖验证码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZjyzCode" runat="server" Text='<%# Bind("ZJMa") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelZjyzCode" runat="server" Text='<%# Bind("ZJMa") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                </ContentTemplate> 
            </asp:UpdatePanel> 
                 </div>
        </form>
         <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>