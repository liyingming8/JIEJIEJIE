<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompanyGoodsTypes.aspx.cs" Inherits="Admin_TJ_CompanyGoodsTypes" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.4.5.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>产品类别</title> 
        <link href="../include/easyui.css" rel="stylesheet" /> 
          <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div class="div_WholePage">
                <div class="topdiv">
                    <div class="topitem"><input type="button" value="新增" class="btn btn-warning btnyd" onclick="openWinCenter('TJ_CompanyGoodsTypesAddEdit.aspx?cmd=add', 400, 300, '系统角色')" /></div>
                    <div class="topitem"><asp:DropDownList ID="DDLField" runat="server">  
                                             <asp:ListItem Value="GoodsType">产品类别</asp:ListItem>
                                             <asp:ListItem Value="Remark">备注</asp:ListItem>
                                         </asp:DropDownList></div>
                     <div class="topitem"><input id="inputSearchKeyword" type="text" class="inputsearch" runat="server" placeholder="请输入查找内容" /></div>
                     <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="btn btn-warning btnyd" onclick="BtnSearch0_Click" /></div> 
                </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" 　 AutoGenerateColumns="False" DataKeyNames="CompGoodsTypeID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="CompGoodsTypeID" DataNavigateUrlFormatString="TJ_CompanyGoodsTypesAddEdit.aspx?cmd=edit&ID={0}" Text="详细" />               
                        <asp:TemplateField HeaderText="产品类别"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelGoodsType" runat="server" Text='<%# Bind("GoodsType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注"> 
                            <ItemTemplate>
                                <asp:Label ID="LabelRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                      <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="20"   NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

            </div>
        </form>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>  
    </body>
</html>