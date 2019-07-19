<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customerbrandinfo.aspx.cs" Inherits="CRM_tj_crm_customerbrandinfo" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    
    <link href="../MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('/crm/tj_crm_customerbrandinfoAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_crm_customerbrandinfo')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="id">id</asp:ListItem>
<asp:ListItem Value="customerid">customerid</asp:ListItem>
<asp:ListItem Value="brandid">brandid</asp:ListItem>
<asp:ListItem Value="startdate">startdate</asp:ListItem>
<asp:ListItem Value="enddate">enddate</asp:ListItem>
<asp:ListItem Value="ispermit">ispermit</asp:ListItem>
<asp:ListItem Value="compid">compid</asp:ListItem>
<asp:ListItem Value="permituserid">permituserid</asp:ListItem>
<asp:ListItem Value="agentlevel">agentlevel</asp:ListItem>
<asp:ListItem Value="natureagentleve">natureagentleve</asp:ListItem>
<asp:ListItem Value="sharetemplateid">sharetemplateid</asp:ListItem>
<asp:ListItem Value="remarks">remarks</asp:ListItem>
             </asp:DropDownList>
        </div>
     
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容" /></div>
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
                <asp:TemplateField HeaderText="customerid">
                    <ItemTemplate>
                        <asp:Label ID="Labelcustomerid" runat="server" Text='<%# Bind("customerid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="brandid">
                    <ItemTemplate>
                        <asp:Label ID="Labelbrandid" runat="server" Text='<%# Bind("brandid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="startdate">
                    <ItemTemplate>
                        <asp:Label ID="Labelstartdate" runat="server" Text='<%# Bind("startdate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="enddate">
                    <ItemTemplate>
                        <asp:Label ID="Labelenddate" runat="server" Text='<%# Bind("enddate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ispermit">
                    <ItemTemplate>
                        <asp:Label ID="Labelispermit" runat="server" Text='<%# Bind("ispermit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="compid">
                    <ItemTemplate>
                        <asp:Label ID="Labelcompid" runat="server" Text='<%# Bind("compid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="permituserid">
                    <ItemTemplate>
                        <asp:Label ID="Labelpermituserid" runat="server" Text='<%# Bind("permituserid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="agentlevel">
                    <ItemTemplate>
                        <asp:Label ID="Labelagentlevel" runat="server" Text='<%# Bind("agentlevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="natureagentleve">
                    <ItemTemplate>
                        <asp:Label ID="Labelnatureagentleve" runat="server" Text='<%# Bind("natureagentleve") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="sharetemplateid">
                    <ItemTemplate>
                        <asp:Label ID="Labelsharetemplateid" runat="server" Text='<%# Bind("sharetemplateid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="remarks">
                    <ItemTemplate>
                        <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
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