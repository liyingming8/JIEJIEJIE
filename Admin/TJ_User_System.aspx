<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_User_System.aspx.cs" Inherits="Admin_TJ_User_System" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="container topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TJ_User_SystemAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'TJ_User_System')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server">
<asp:ListItem Value="UserID">UserID</asp:ListItem>
<asp:ListItem Value="IdentityCode">IdentityCode</asp:ListItem>
<asp:ListItem Value="LoginName">LoginName</asp:ListItem>
<asp:ListItem Value="PassWords">PassWords</asp:ListItem>
<asp:ListItem Value="NickName">NickName</asp:ListItem>
<asp:ListItem Value="SexInfo">SexInfo</asp:ListItem>
<asp:ListItem Value="RegisterDate">RegisterDate</asp:ListItem>
<asp:ListItem Value="AddressInfo">AddressInfo</asp:ListItem>
<asp:ListItem Value="PostCode">PostCode</asp:ListItem>
<asp:ListItem Value="SystemPermission">SystemPermission</asp:ListItem>
<asp:ListItem Value="IntegralValue">IntegralValue</asp:ListItem>
<asp:ListItem Value="Remarks">Remarks</asp:ListItem>
<asp:ListItem Value="WXGongZhongHao">WXGongZhongHao</asp:ListItem>
<asp:ListItem Value="WXDengLuYouXiang">WXDengLuYouXiang</asp:ListItem>
<asp:ListItem Value="WXYuanShiID">WXYuanShiID</asp:ListItem>
<asp:ListItem Value="WXNumber">WXNumber</asp:ListItem>
<asp:ListItem Value="WXLeiXing">WXLeiXing</asp:ListItem>
<asp:ListItem Value="WXRenZhengQingKuang">WXRenZhengQingKuang</asp:ListItem>
<asp:ListItem Value="WXToken">WXToken</asp:ListItem>
<asp:ListItem Value="WXSignature">WXSignature</asp:ListItem>
<asp:ListItem Value="WXTimesStamp">WXTimesStamp</asp:ListItem>
<asp:ListItem Value="WXOnece">WXOnece</asp:ListItem>
<asp:ListItem Value="WXEchoStrnig">WXEchoStrnig</asp:ListItem>
<asp:ListItem Value="WXIsYanZheng">WXIsYanZheng</asp:ListItem>
<asp:ListItem Value="HeaderImageUrl">HeaderImageUrl</asp:ListItem>
<asp:ListItem Value="AuthorDiscount">AuthorDiscount</asp:ListItem>
<asp:ListItem Value="MobileNumber">MobileNumber</asp:ListItem>
<asp:ListItem Value="WX_Province">WX_Province</asp:ListItem>
<asp:ListItem Value="WX_City">WX_City</asp:ListItem>
<asp:ListItem Value="reg_date">reg_date</asp:ListItem>
             </asp:DropDownList>
        </div>
     <div class="topitem"><span>包含</span></div>
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
</div>
<div>
<asp:GridView ID="GridView1" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="UserID"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField>
                <asp:TemplateField HeaderText="ParentID">
                    <ItemTemplate>
                        <asp:Label ID="LabelParentID" runat="server" Text='<%# Bind("ParentID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CompID">
                    <ItemTemplate>
                        <asp:Label ID="LabelCompID" runat="server" Text='<%# Bind("CompID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RID">
                    <ItemTemplate>
                        <asp:Label ID="LabelRID" runat="server" Text='<%# Bind("RID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IdentityCode">
                    <ItemTemplate>
                        <asp:Label ID="LabelIdentityCode" runat="server" Text='<%# Bind("IdentityCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LoginName">
                    <ItemTemplate>
                        <asp:Label ID="LabelLoginName" runat="server" Text='<%# Bind("LoginName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PassWords">
                    <ItemTemplate>
                        <asp:Label ID="LabelPassWords" runat="server" Text='<%# Bind("PassWords") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NickName">
                    <ItemTemplate>
                        <asp:Label ID="LabelNickName" runat="server" Text='<%# Bind("NickName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SexInfo">
                    <ItemTemplate>
                        <asp:Label ID="LabelSexInfo" runat="server" Text='<%# Bind("SexInfo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RegisterDate">
                    <ItemTemplate>
                        <asp:Label ID="LabelRegisterDate" runat="server" Text='<%# Bind("RegisterDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IsActived">
                    <ItemTemplate>
                        <asp:Label ID="LabelIsActived" runat="server" Text='<%# Bind("IsActived") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FromCityID">
                    <ItemTemplate>
                        <asp:Label ID="LabelFromCityID" runat="server" Text='<%# Bind("FromCityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AddressInfo">
                    <ItemTemplate>
                        <asp:Label ID="LabelAddressInfo" runat="server" Text='<%# Bind("AddressInfo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PostCode">
                    <ItemTemplate>
                        <asp:Label ID="LabelPostCode" runat="server" Text='<%# Bind("PostCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SystemPermission">
                    <ItemTemplate>
                        <asp:Label ID="LabelSystemPermission" runat="server" Text='<%# Bind("SystemPermission") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IntegralValue">
                    <ItemTemplate>
                        <asp:Label ID="LabelIntegralValue" runat="server" Text='<%# Bind("IntegralValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXGongZhongHao">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXGongZhongHao" runat="server" Text='<%# Bind("WXGongZhongHao") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXDengLuYouXiang">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXDengLuYouXiang" runat="server" Text='<%# Bind("WXDengLuYouXiang") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXYuanShiID">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXYuanShiID" runat="server" Text='<%# Bind("WXYuanShiID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXNumber">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXNumber" runat="server" Text='<%# Bind("WXNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXLeiXing">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXLeiXing" runat="server" Text='<%# Bind("WXLeiXing") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXRenZhengQingKuang">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXRenZhengQingKuang" runat="server" Text='<%# Bind("WXRenZhengQingKuang") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXToken">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXToken" runat="server" Text='<%# Bind("WXToken") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXSignature">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXSignature" runat="server" Text='<%# Bind("WXSignature") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXTimesStamp">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXTimesStamp" runat="server" Text='<%# Bind("WXTimesStamp") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXOnece">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXOnece" runat="server" Text='<%# Bind("WXOnece") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXEchoStrnig">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXEchoStrnig" runat="server" Text='<%# Bind("WXEchoStrnig") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WXIsYanZheng">
                    <ItemTemplate>
                        <asp:Label ID="LabelWXIsYanZheng" runat="server" Text='<%# Bind("WXIsYanZheng") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HeaderImageUrl">
                    <ItemTemplate>
                        <asp:Label ID="LabelHeaderImageUrl" runat="server" Text='<%# Bind("HeaderImageUrl") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AuthorDiscount">
                    <ItemTemplate>
                        <asp:Label ID="LabelAuthorDiscount" runat="server" Text='<%# Bind("AuthorDiscount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MobileNumber">
                    <ItemTemplate>
                        <asp:Label ID="LabelMobileNumber" runat="server" Text='<%# Bind("MobileNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WX_Province">
                    <ItemTemplate>
                        <asp:Label ID="LabelWX_Province" runat="server" Text='<%# Bind("WX_Province") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WX_City">
                    <ItemTemplate>
                        <asp:Label ID="LabelWX_City" runat="server" Text='<%# Bind("WX_City") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="reg_date">
                    <ItemTemplate>
                        <asp:Label ID="Labelreg_date" runat="server" Text='<%# Bind("reg_date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="reg_year">
                    <ItemTemplate>
                        <asp:Label ID="Labelreg_year" runat="server" Text='<%# Bind("reg_year") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="reg_month">
                    <ItemTemplate>
                        <asp:Label ID="Labelreg_month" runat="server" Text='<%# Bind("reg_month") %>'></asp:Label>
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
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>