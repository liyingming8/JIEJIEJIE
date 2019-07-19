<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiQuAndChengQu.aspx.cs" Inherits="Admin_TB_SuYuanZhiQuAndChengQu" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TB_SuYuanZhiQuAndChengQuAddEdit.aspx?CQID=<%=Sc.EncryptQueryString(HF_CQID.Value)%>&cmd=<%=Sc.EncryptQueryString("add")%>', 600,450,'原曲配料')"/></div>   　
</div>
<div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="ID"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField> 
                <asp:TemplateField HeaderText="成曲批次">
                    <ItemTemplate>
                        <asp:Label ID="LabelCQID" runat="server" Text='<%# BtbSuYuanChengQu.GetList(int.Parse(Eval("CQID").ToString())).CQPC %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="原曲批次">
                    <ItemTemplate>
                        <asp:Label ID="LabelQLID" runat="server" Text='<%# BtbSuYuanZhiQu.GetList(int.Parse(Eval("QLID").ToString())).ZhiQuPC  %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="含量">
                    <ItemTemplate>
                        <asp:Label ID="LabelPercentValue" runat="server" Text='<%# Bind("PercentValue") %>'></asp:Label>%
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle  HorizontalAlign="Center" Width="50px" /></asp:CommandField>
</Columns>
<EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
<FooterStyle CssClass="GridViewFooterStyle" />
<RowStyle CssClass="GridViewRowStyle" />
<SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
<webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="20" OnPageChanging="AspNetPager1_PageChanging"  NumericButtonCount="5"  CustomInfoHTML="" CustomInfoSectionWidth=""  NextPageText="下一页" PrevPageText="上一页"  ></webdiyer:AspNetPager>
 <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
    <asp:HiddenField ID="HF_CQID" runat="server" />
</form>
</body>
</html>