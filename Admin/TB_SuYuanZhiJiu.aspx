<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiJiu.aspx.cs" Inherits="Admin_TB_SuYuanZhiJiu" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
        <div class="div_WholePage">
<div class="topdiv"> 
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TB_SuYuanZhiJiuAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 550,430,'制酒信息')"/></div>  
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server"> 
                <asp:ListItem Value="ZJPC">制酒批次</asp:ListItem> 
                <asp:ListItem Value="Remarks">备注</asp:ListItem>
             </asp:DropDownList>
        </div> 
     <div class="topitem"><input id="inputSearchKeyword" placeholder="请输入查找内容" type="text" runat="server" class="inputsearch" /></div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
    
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
                <asp:TemplateField HeaderText="制酒批次">
                    <ItemTemplate>
                        <asp:Label ID="LabelZJPC" runat="server" Text='<%# Bind("ZJPC") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="制酒日期">
                    <ItemTemplate>
                        <asp:Label ID="LabelZhiJiuTime" runat="server" Text='<%# Bind("ZhiJiuTime","{0:yyyy-MM-dd}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="成曲">
                      <ItemTemplate>
                          <asp:HyperLink ID="hyplink" CssClass="btn btn-default btnydinline"  runat="server">配料...</asp:HyperLink>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="原粮">
                      <ItemTemplate>
                          <asp:HyperLink ID="hyplinkyl" CssClass="btn btn-default btnydinline"  runat="server">配料...</asp:HyperLink>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="库房">
                    <ItemTemplate>
                        <asp:Label ID="LabelCangKuID" runat="server" Text='<%# BtbSuYuanKuFang.GetList(int.Parse(Eval("CangKuID").ToString())).KuFang %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="质检员">
                    <ItemTemplate>
                        <asp:Label ID="LabelZhiJianYuanID" runat="server" Text='<%# BtbSuYuanZhiJianInfo.GetList(int.Parse(Eval("ZhiJianYuanID").ToString())).ZhiJianName %>'></asp:Label>
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
</div>
    <script src="../js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/jquery.easyui.min.js"></script>
</form>
</body>
</html>