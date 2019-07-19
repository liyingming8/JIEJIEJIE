<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_Products_Inforjg.aspx.cs" Inherits="Admin_TB_Products_Inforjg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="Product_Code">产品编码</asp:ListItem>
                                             <asp:ListItem Value="Products_Name">产品名称</asp:ListItem>
                                             <asp:ListItem Value="Products_Summary">简介</asp:ListItem>
                                             <asp:ListItem Value="Remarks">备注</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server" class="inputsearch"  placeholder="请输入查找内容"  /><asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" onclick="BtnSearch0_Click" /></td><td>相关操作:</td><td><a href="TB_Products_InforAddEditjg.aspx?cmd=add"><img title="添加" src="../../images/add.png" border="0"></a></td></tr></table>
                <br />
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 AutoGenerateColumns="False" DataKeyNames="Infor_ID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" >
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="Infor_ID" DataNavigateUrlFormatString="TB_Products_InforAddEditjg.aspx?cmd=edit&ID={0}" Text="详细" />
                        <asp:TemplateField HeaderText="产品类别">
                            <ItemTemplate>
                                <asp:Label ID="LabelTypeId" runat="server" Text='<%# (Eval("TypeId").Equals(null) ? "" : ptype.GetList(Convert.ToInt32(Eval("TypeId"))).TypeName) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="规格">
                            <ItemTemplate>
                                <asp:Label ID="LabelPSID" runat="server" Text='<%# (Eval("PSID").Equals(null) ? "" : pstandards.GetList(Convert.ToInt32(Eval("PSID").ToString())).StandarsDes) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品编码">
                            <ItemTemplate>
                                <asp:Label ID="LabelProduct_Code" runat="server" Text='<%# Bind("Product_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品名称">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProducts_Name" runat="server"  Text='<%# Bind("Products_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelProducts_Name" runat="server" Text='<%# Bind("Products_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>              
                        <asp:TemplateField HeaderText="香型">                 
                            <ItemTemplate>
                                <asp:Label ID="LabelProductXiangXing" runat="server" Text='<%# (Eval("ProductXiangXing").Equals(null) ? "" : pxiangxing.GetList(Convert.ToInt32(Eval("ProductXiangXing").ToString())).XiangXing) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="酒精度">
                            <ItemTemplate>
                                <asp:Label ID="LabelProductJiuJingDu" runat="server" Text='<%# (Eval("ProductJiuJingDu").Equals(null) ? "" : pjiujingdu.GetList(Convert.ToInt32(Eval("ProductJiuJingDu").ToString())).JiuJingDuShu) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="净含量">
                            <ItemTemplate>
                                <asp:Label ID="LabelProductJingHanLiang" runat="server" Text='<%# (Eval("ProductJingHanLiang").Equals(null) ? "" : pjinghanliang.GetList(Convert.ToInt32(Eval("ProductJingHanLiang").ToString())).JingHanLiang) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="产品简介">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProducts_Summary" runat="server" Text='<%# Bind("Products_Summary") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelProducts_Summary" runat="server" Text='<%# Bind("Products_Summary") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="自主品牌">
                            <ItemTemplate>
                                <asp:CheckBox ID="checkboxisown" runat="server" Enabled="false" Checked='<%# Bind("IsOwn") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'  MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="原料">
                            <ItemTemplate>
                                <asp:Label ID="Labelyuanliao" runat="server" Text='<%# (Eval("MTID").Equals(null) ? "" : byuanliao.GetList(Convert.ToInt32(Eval("MTID").ToString())).MTname) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="标准">
                            <ItemTemplate>
                                <asp:Label ID="Labelbiaozhun" runat="server" Text='<%# (Eval("BZID").Equals(null) ? "" : bbiaozhun.GetList(Convert.ToInt32(Eval("BZID").ToString())).BiaoZhunname) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                     
                        <%--<asp:CommandField ShowEditButton="True" />--%>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>