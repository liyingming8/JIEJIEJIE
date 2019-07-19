<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_rewardprogram.aspx.cs" Inherits="CRM_tj_crm_rewardprogram" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/> 
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="div_WholePage">
<div class="topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('/crm/tj_crm_rewardprogramAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'层级返利')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<%--<asp:ListItem Value="id">id</asp:ListItem>--%>
<asp:ListItem Value="parentgid">父级</asp:ListItem>
<asp:ListItem Value="childgid">子级</asp:ListItem>
 
<asp:ListItem Value="rewardtype">奖励类型</asp:ListItem>
 
             </asp:DropDownList>
        </div>
     <div class="topitem"><span>包含</span></div>
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
</div>
<div style="overflow-x: auto">
<asp:GridView ID="GridView1" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField>
          <asp:TemplateField HeaderText="子级">
                    <ItemTemplate>
                        <asp:Label ID="Labelchildgid" runat="server" Text='<%# Bind("cnm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Label ID="label" runat="server" Text='向'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="父级">
                    <ItemTemplate>
                        <asp:Label ID="Labelparentgid" runat="server" Text='<%# Bind("pnm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="类型">
                    <ItemTemplate>
                        <asp:Label ID="Labelrewardtype" runat="server" Text='<%# Bind("renm") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="金额">
                    <ItemTemplate>
                        ￥<asp:Label ID="Labelrewardnum" runat="server" Text='<%# Bind("rewardnum") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="关系">
                    <ItemTemplate>
                        <asp:Label ID="Labelgradetype" runat="server" Text='<%# Bind("gradetype") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle CssClass="btn btn-default btnydinlineforgridview" HorizontalAlign="Center" Width="50px" /></asp:CommandField>
</Columns>
<EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
<FooterStyle CssClass="GridViewFooterStyle" />
<RowStyle CssClass="GridViewRowStyle" />
<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView>
<webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="GridViewPagerStyle"  PageSize="20" OnPageChanging="AspNetPager1_PageChanging" ustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"></webdiyer:AspNetPager>
</div>
        </div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>