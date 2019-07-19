<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_brandprice.aspx.cs" Inherits="CRM_tj_crm_brandprice" %>
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
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('/crm/tj_crm_brandpriceAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 580,420,'品牌价格');"/></div><div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server"> 
                    <asp:ListItem Value="remarks">备注</asp:ListItem>
             </asp:DropDownList>
        </div>
     
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容" /></div>
     <div class="topitem"><span>品牌</span></div>
     <div class="topitem"><asp:DropDownList runat="server" ID="ddlbrand" DataTextField="brandname" DataValueField="id" AppendDataBoundItems="True">
         <asp:ListItem Selected="True" Value="0">全部...</asp:ListItem>
         </asp:DropDownList>
    </div>
     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div>
</div>
<div>
<div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" AutoGenerateColumns="False" DataKeyNames="id"
onrowdeleting="GridView1_RowDeleting" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
<Columns>
    <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="../Admin/image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
         <asp:TemplateField HeaderText="序号">
               <ItemTemplate>
                    <asp:Label ID="LabelIndex" runat="server" ></asp:Label>
                </ItemTemplate>
             <ItemStyle HorizontalAlign="Center" Width="60px" />
         </asp:TemplateField>
                <asp:TemplateField HeaderText="品牌">
                    <ItemTemplate>
                        <asp:Label ID="Labelbrandid" runat="server" Text='<%# GetBrandName(Eval("brandid").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="代理级别">
                    <ItemTemplate>
                        <asp:Label ID="Labelcustomerid" runat="server" Text='<%# GetCustomerGradeName(Eval("cgradeid").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="起订数量">
                    <ItemTemplate>
                        <asp:Label ID="Labelstartvalue" runat="server" Text='<%# Eval("startvalue").ToString() %>'></asp:Label><asp:Label ID="Labelunitid1" runat="server" Text='<%# GetUnitName(Eval("unitid").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="价格">
                    <ItemTemplate>
                        ￥<asp:Label ID="Labelorignalprice" runat="server" Text='<%# Bind("orignalprice") %>'></asp:Label>/<asp:Label ID="Labelunitid" runat="server" Text='<%# GetUnitName(Eval("unitid").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="奖励额度">
                    <ItemTemplate>
                        ￥<asp:Label ID="Labelcommissionvalue" runat="server" Text='<%# Bind("commissionvalue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="父级返点">
                    <ItemTemplate>
                        <asp:Label ID="Labelparentrcommissionpercent" runat="server" Text='<%# Convert.ToDecimal(Eval("parentrcommissionpercent"))*100 %>'></asp:Label>%
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="返点级数">
                    <ItemTemplate>
                        <asp:Label ID="Labelinputdate" runat="server" Text='<%# Bind("commisiongradenumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="inputuserid">
                    <ItemTemplate>
                        <asp:Label ID="Labelinputuserid" runat="server" Text='<%# Bind("inputuserid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="修改时间">
                    <ItemTemplate>
                        <asp:Label ID="Labellastupdatedate" runat="server" Text='<%# Bind("lastupdatedate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="修改人">
                    <ItemTemplate>
                        <asp:Label ID="Labellastupdateuserid" runat="server" Text='<%# BtjUser.GetList(int.Parse(Eval("lastupdateuserid").ToString())).LoginName %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> --%>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
         <%--
         <asp:CommandField  ShowDeleteButton="True" ><ItemStyle HorizontalAlign="Center" Width="50px" /></asp:CommandField>--%>
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