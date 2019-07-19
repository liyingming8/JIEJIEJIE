<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_customergrade.aspx.cs" Inherits="crm_tj_crm_customergrade" %>
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
    <div class="topitem"><input id="add" type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('/crm/tj_crm_customergradeAddEdit.aspx?cmd=<%=Sc.EncryptQueryString("add")%>', 650,420,'经销商级别')"/></div>        <div class="topitem"><span></span></div>
        <div class="topitem">
             <asp:DropDownList ID="DDLField" runat="server"> 
                <asp:ListItem Value="gradename" Selected="True">经销商级别</asp:ListItem> 
             </asp:DropDownList>
        </div>
     
     <div class="topitem"><input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容" /></div>
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
                <asp:TemplateField HeaderText="经销商级别">
                    <ItemTemplate>
                        <asp:Label ID="Labelgradename" runat="server" Text='<%# Bind("gradename") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="排序">
                    <ItemTemplate>
                        <asp:Label ID="Labelgradeorder" runat="server" Text='<%# Bind("gradeorder") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="祖级审核">
                                <ItemTemplate>
                                    <asp:Label ID="Labelfirstcheck" runat="server" Text='<%# Convert.ToBoolean(Eval("firstcheck").Equals(DBNull.Value)?"false":Eval("firstcheck").ToString())?"√":"—" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                <asp:TemplateField HeaderText="营业执照">
                             <ItemTemplate>
                                    <asp:Label ID="Labelblicenses" runat="server" Text='<%# Convert.ToBoolean(Eval("blicenses").Equals(DBNull.Value)?"false":Eval("blicenses").ToString())?"√":"—" %>'></asp:Label>
                                </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
               <asp:TemplateField HeaderText="授权级别">
                   <ItemTemplate>
                       <asp:HyperLink ID="hplinkchildagent" CssClass="btn btn-info btnyd" runat="server">授权</asp:HyperLink>
                   </ItemTemplate> 
                   <ItemStyle HorizontalAlign="Center" />
               </asp:TemplateField>
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