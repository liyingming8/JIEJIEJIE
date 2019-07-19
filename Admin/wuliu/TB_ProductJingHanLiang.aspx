<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_ProductJingHanLiang.aspx.cs" Inherits="Admin_TB_ProductJingHanLiang" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
     <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="../../include/easyui.css" rel="stylesheet" />
  
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="div_WholePage">
                <div class="topdiv">
                    <div class="topitem"><input type="button" class="btn btn-warning btnyd" value="新增" onclick="openWinCenter('TB_ProductJingHanLiangAddEdit.aspx?cmd=add', 450, 300, '净含量编辑')" /></div>
                    <div class="topitem"><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="JingHanLiang">净含量</asp:ListItem>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList></div>
                    
                    <div class="topitem">
                        <input id="inputSearchKeyword" class="inputsearch" type="text" placeholder="请输入查找内容"  runat="server" />
                    </div>
                    <div class="topitem"><asp:Button ID="BtnSearch0" CssClass="btn btn-warning btnyd" runat="server" Text="查找"  onclick="BtnSearch0_Click" /></div> 
                          </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="False"　  AutoGenerateColumns="False" DataKeyNames="ID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" > 
                     <Columns>
                         <asp:TemplateField HeaderText="编辑">
                            <ItemTemplate>
                                <img src="../image/edit.png" alt="Edit" height="25" id="editimg" runat="server"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="净含量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtJingHanLiang" runat="server"  Text='<%# Bind("JingHanLiang") %>' MaxLength="30"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelJingHanLiang" runat="server" Text='<%# Bind("JingHanLiang") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"  Text='<%# Bind("Remarks") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>               
                        <%--<asp:CommandField ShowEditButton="True" />--%>
                        <%-- <asp:CommandField ShowDeleteButton="True" />--%>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                    <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="15"   NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>
            </div>
        </form>
         <script src="../../include/js/jquery.min.js" type="text/javascript"></script> 
        <script type="text/javascript" src="../../include/js/UploadImage.js"></script>
        <script src="../../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>