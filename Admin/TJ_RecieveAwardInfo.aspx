<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RecieveAwardInfo.aspx.cs" Inherits="Admin_TJ_RecieveAwardInfo" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" defaultbutton="BtnSearch0" runat="server">
            <div>
                <table  class="tdbg">
                    <tr> 
                        <td><asp:DropDownList ID="DDLField" runat="server" 
                                                           meta:resourcekey="DDLFieldResource1">
                                             <asp:ListItem Value="RAWID" meta:resourcekey="ListItemResource1">RAWID</asp:ListItem>
                                             <asp:ListItem Value="NeedIntegralValue" meta:resourcekey="ListItemResource2">NeedIntegralValue</asp:ListItem>
                                             <asp:ListItem Value="ReceiverName" meta:resourcekey="ListItemResource3">ReceiverName</asp:ListItem>
                                             <asp:ListItem Value="DetailAddress" meta:resourcekey="ListItemResource4">DetailAddress</asp:ListItem>
                                             <asp:ListItem Value="PhoneNumber" meta:resourcekey="ListItemResource5">PhoneNumber</asp:ListItem>
                                             <asp:ListItem Value="RecieveDate" meta:resourcekey="ListItemResource6">RecieveDate</asp:ListItem>
                                             <asp:ListItem Value="Remarks" meta:resourcekey="ListItemResource7">Remarks</asp:ListItem>
                                         </asp:DropDownList>
                            包含<input id="inputSearchKeyword" type="text" runat="server"  placeholder="请输入查找内容"  class="inputsearch" /><asp:Button 
                                                                                                                            ID="BtnSearch0" runat="server" Text="查找" CssClass="inputbutton" 
                                                                                                                            onclick="BtnSearch0_Click" meta:resourcekey="BtnSearch0Resource1" /></td><td>相关操作:</td><td><a href="TJ_RecieveAwardInfoAddEdit.aspx?cmd=add"><img title="添加" src="images/add.png" border="0"></a></td></tr></table>
                 
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server" AllowPaging="True"　 
                              AutoGenerateColumns="False" DataKeyNames="RAWID"
                              onpageindexchanging="GridView1_PageIndexChanging"   
                              onrowcancelingedit="GridView1_RowCancelingEdit"  
                              onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                              onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                              CssClass="GridViewStyle" PageSize="18" 
                              meta:resourcekey="GridView1Resource1" >
                    <Columns>
                        <asp:TemplateField HeaderText="活动" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:Label ID="LabelITGRID" runat="server" 
                                           Text='<%# ReturnIntegralName(Eval("ITGRID").ToString()) %>' 
                                           meta:resourcekey="LabelITGRIDResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="奖品" meta:resourcekey="TemplateFieldResource2">                   
                            <ItemTemplate>
                                <asp:Image ImageUrl='<%# ReturnAwardName(Eval("AWID").ToString()) %>' 
                                           runat="server" meta:resourcekey="ImageResource1" />                         
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="系统用户" meta:resourcekey="TemplateFieldResource3">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelUserID" runat="server" 
                                           Text='<%# ReturnUserName(Eval("UserID").ToString()) %>' 
                                           meta:resourcekey="LabelUserIDResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="消费积分" meta:resourcekey="TemplateFieldResource4">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelNeedIntegralValue" runat="server" 
                                           Text='<%# Bind("NeedIntegralValue") %>' 
                                           meta:resourcekey="LabelNeedIntegralValueResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货人" meta:resourcekey="TemplateFieldResource5">                  
                            <ItemTemplate>
                                <asp:Label ID="LabelReceiverName" runat="server" 
                                           Text='<%# Bind("ReceiverName") %>' 
                                           meta:resourcekey="LabelReceiverNameResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="收货地址" meta:resourcekey="TemplateFieldResource6">                   
                            <ItemTemplate>
                                <asp:Label ID="LabelDetailAddress" runat="server" 
                                           Text='<%# Bind("DetailAddress") %>' 
                                           meta:resourcekey="LabelDetailAddressResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="电话号码" meta:resourcekey="TemplateFieldResource7">
                            <ItemTemplate>
                                <asp:Label ID="LabelPhoneNumber" runat="server" 
                                           Text='<%# Bind("PhoneNumber") %>' meta:resourcekey="LabelPhoneNumberResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="兑奖日期" meta:resourcekey="TemplateFieldResource8">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRecieveDate" runat="server"  MaxLength="16" 
                                             meta:resourcekey="txtRecieveDateResource1"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="LabelRecieveDate" runat="server" 
                                           Text='<%# Bind("RecieveDate", "{0:yyyy-MM-dd}") %>' 
                                           meta:resourcekey="LabelRecieveDateResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注" meta:resourcekey="TemplateFieldResource9">                    
                            <ItemTemplate>
                                <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>' 
                                           meta:resourcekey="LabelRemarksResource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
            </div>
        </form>
    </body>
</html>