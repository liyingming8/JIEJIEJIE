<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DJJGozb.aspx.cs" Inherits="TJ_DJJGozb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        <style type="text/css">
            .auto-style1 { width: 298px; }
        </style>
    </head>
    <body>
        <form id="form1"  runat="server">
            <div>
                <table  class="tdbg">
                    <tr><td>; 手机号码<b>：</b></td>
                        <td >;<input id="Mphone"  style="height: 20px !important" type="text" runat="server" class="inputtext" /></td><td class="tdbg" >
                          </td><td align="center" class="tdbg" ><asp:Button ID="BtnSearch2" runat="server" Text="查询" CssClass="inputbutton" OnClick="BtnSearch2_Click" Width="124px" Font-Size="Large" Height="38px"  /></td><td>
                                                                                                                                                                                                                                                                                                                                                                                           <asp:Button ID="Button1" runat="server" Text="兑奖" CssClass="inputbutton" Width="133px" OnClick="Button1_Click"  Font-Size="Large" Height="36px"  Enabled="false"  /></td></tr></table>
                 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" 
                                      AutoGenerateColumns="False" DataKeyNames="ID,CompID" onpageindexchanging="GridView1_PageIndexChanging"   
                                      onrowcancelingedit="GridView1_RowCancelingEdit"  
                                      onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  
                                      onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" 
                                      CssClass="GridViewStyle" PageSize="18" >
                            <Columns>             
                                <asp:TemplateField HeaderText="标签序号">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelBoxLabel" runat="server" Text='<%# Bind("BoxLabel") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="验证码">  
                            
                                    <ItemTemplate>
                                        <asp:Label ID="LabelYZM" runat="server" Text='<%# Bind("YZM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>               
                                <asp:TemplateField HeaderText="所中奖项">                    
                                    <ItemTemplate>
                                        <asp:Label ID="JXname" runat="server" Text='<%# Bind("JXname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                
                                <asp:TemplateField HeaderText="奖品">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelJPType" runat="server" Text='<%# Bind("JPType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="中奖人">                  
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWXname" runat="server" Text='<%# Bind("WXname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="中奖时间">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LQtime" runat="server" Text='<%# Bind("LQtime", "{0:yyyy-MM-dd }") %>'></asp:Label>
                                        <asp:Label ID="LabelWXname" runat="server" Text='<%# Bind("LQtime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="是否领奖">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Eval("LJflag").ToString() == "1" ? "已领奖" : "未领奖" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="兑奖人">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# ReturnLoginName(Eval("DHUserID").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
         
         

                
                                <asp:TemplateField HeaderText="备注">                   
                                    <ItemTemplate>
                                        <asp:Label ID="LabelRemarks" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </form>
    </body>
</html>