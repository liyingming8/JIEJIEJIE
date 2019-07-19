<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_js_user.aspx.cs" Inherits="Admin_tj_js_user" %>
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
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('tj_js_userAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'tj_js_user')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="userid">userid</asp:ListItem>
<asp:ListItem Value="name">name</asp:ListItem>
<asp:ListItem Value="nickname">nickname</asp:ListItem>
<asp:ListItem Value="password">password</asp:ListItem>
<asp:ListItem Value="sex">sex</asp:ListItem>
<asp:ListItem Value="cellphone">cellphone</asp:ListItem>
<asp:ListItem Value="telphone">telphone</asp:ListItem>
<asp:ListItem Value="address">address</asp:ListItem>
<asp:ListItem Value="city">city</asp:ListItem>
<asp:ListItem Value="idcard">idcard</asp:ListItem>
<asp:ListItem Value="postcode">postcode</asp:ListItem>
<asp:ListItem Value="headpic">headpic</asp:ListItem>
<asp:ListItem Value="qianming">qianming</asp:ListItem>
<asp:ListItem Value="deliveryname">deliveryname</asp:ListItem>
<asp:ListItem Value="deliveryphone">deliveryphone</asp:ListItem>
<asp:ListItem Value="deliveryaddress">deliveryaddress</asp:ListItem>
<asp:ListItem Value="deliverycity">deliverycity</asp:ListItem>
<asp:ListItem Value="likes">likes</asp:ListItem>
<asp:ListItem Value="compid">compid</asp:ListItem>
<asp:ListItem Value="registerdate">registerdate</asp:ListItem>
<asp:ListItem Value="wxgongzhonghao">wxgongzhonghao</asp:ListItem>
<asp:ListItem Value="wxopenid">wxopenid</asp:ListItem>
             </asp:DropDownList>
        </div>
     
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
</div>
<div>
<div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="userid"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField>
                <asp:TemplateField HeaderText="name">
                    <ItemTemplate>
                        <asp:Label ID="Labelname" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="nickname">
                    <ItemTemplate>
                        <asp:Label ID="Labelnickname" runat="server" Text='<%# Bind("nickname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="password">
                    <ItemTemplate>
                        <asp:Label ID="Labelpassword" runat="server" Text='<%# Bind("password") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="sex">
                    <ItemTemplate>
                        <asp:Label ID="Labelsex" runat="server" Text='<%# Bind("sex") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="cellphone">
                    <ItemTemplate>
                        <asp:Label ID="Labelcellphone" runat="server" Text='<%# Bind("cellphone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="telphone">
                    <ItemTemplate>
                        <asp:Label ID="Labeltelphone" runat="server" Text='<%# Bind("telphone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="address">
                    <ItemTemplate>
                        <asp:Label ID="Labeladdress" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="city">
                    <ItemTemplate>
                        <asp:Label ID="Labelcity" runat="server" Text='<%# Bind("city") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="idcard">
                    <ItemTemplate>
                        <asp:Label ID="Labelidcard" runat="server" Text='<%# Bind("idcard") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="postcode">
                    <ItemTemplate>
                        <asp:Label ID="Labelpostcode" runat="server" Text='<%# Bind("postcode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="headpic">
                    <ItemTemplate>
                        <asp:Label ID="Labelheadpic" runat="server" Text='<%# Bind("headpic") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="qianming">
                    <ItemTemplate>
                        <asp:Label ID="Labelqianming" runat="server" Text='<%# Bind("qianming") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliveryname">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliveryname" runat="server" Text='<%# Bind("deliveryname") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliveryphone">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliveryphone" runat="server" Text='<%# Bind("deliveryphone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliveryaddress">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliveryaddress" runat="server" Text='<%# Bind("deliveryaddress") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="deliverycity">
                    <ItemTemplate>
                        <asp:Label ID="Labeldeliverycity" runat="server" Text='<%# Bind("deliverycity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="likes">
                    <ItemTemplate>
                        <asp:Label ID="Labellikes" runat="server" Text='<%# Bind("likes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="compid">
                    <ItemTemplate>
                        <asp:Label ID="Labelcompid" runat="server" Text='<%# Bind("compid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="registerdate">
                    <ItemTemplate>
                        <asp:Label ID="Labelregisterdate" runat="server" Text='<%# Bind("registerdate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wxgongzhonghao">
                    <ItemTemplate>
                        <asp:Label ID="Labelwxgongzhonghao" runat="server" Text='<%# Bind("wxgongzhonghao") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="wxopenid">
                    <ItemTemplate>
                        <asp:Label ID="Labelwxopenid" runat="server" Text='<%# Bind("wxopenid") %>'></asp:Label>
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