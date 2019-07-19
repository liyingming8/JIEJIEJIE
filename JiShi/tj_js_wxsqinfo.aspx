<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_wxsqinfo.aspx.cs" Inherits="Admin_tj_js_wxsqinfo" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('tj_js_wxsqinfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_js_wxsqinfo')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="id">id</asp:ListItem>
<asp:ListItem Value="compid">compid</asp:ListItem>
<asp:ListItem Value="sid">sid</asp:ListItem>
<asp:ListItem Value="wx_appid">wx_appid</asp:ListItem>
<asp:ListItem Value="wx_appsecret">wx_appsecret</asp:ListItem>
<asp:ListItem Value="wx_redirect_url">wx_redirect_url</asp:ListItem>
<asp:ListItem Value="wx_cl_url">wx_cl_url</asp:ListItem>
<asp:ListItem Value="wx_scope">wx_scope</asp:ListItem>
<asp:ListItem Value="wx_gz">wx_gz</asp:ListItem>
<asp:ListItem Value="ownurl">ownurl</asp:ListItem>
<asp:ListItem Value="remarkes">remarkes</asp:ListItem>
             </asp:DropDownList>
        </div>
     
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
</div>
<div>
<div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField>
                <asp:TemplateField HeaderText="compid">
                    <ItemTemplate>
                        <asp:Label ID="Labelcompid" runat="server" Text='<%# Bind("compid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="sid">
                    <ItemTemplate>
                        <asp:Label ID="Labelsid" runat="server" Text='<%# Bind("sid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wx_appid">
                    <ItemTemplate>
                        <asp:Label ID="Labelwx_appid" runat="server" Text='<%# Bind("wx_appid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wx_appsecret">
                    <ItemTemplate>
                        <asp:Label ID="Labelwx_appsecret" runat="server" Text='<%# Bind("wx_appsecret") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wx_redirect_url">
                    <ItemTemplate>
                        <asp:Label ID="Labelwx_redirect_url" runat="server" Text='<%# Bind("wx_redirect_url") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wx_cl_url">
                    <ItemTemplate>
                        <asp:Label ID="Labelwx_cl_url" runat="server" Text='<%# Bind("wx_cl_url") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wx_scope">
                    <ItemTemplate>
                        <asp:Label ID="Labelwx_scope" runat="server" Text='<%# Bind("wx_scope") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wx_gz">
                    <ItemTemplate>
                        <asp:Label ID="Labelwx_gz" runat="server" Text='<%# Bind("wx_gz") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ownurl">
                    <ItemTemplate>
                        <asp:Label ID="Labelownurl" runat="server" Text='<%# Bind("ownurl") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="remarkes">
                    <ItemTemplate>
                        <asp:Label ID="Labelremarkes" runat="server" Text='<%# Bind("remarkes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle  HorizontalAlign="Center" Width="50px" /></asp:CommandField>
</Columns>
<EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
<FooterStyle CssClass="GridViewFooterStyle" />
<RowStyle CssClass="GridViewRowStyle" />
<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
<webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="20" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  ></webdiyer:AspNetPager>
</div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>