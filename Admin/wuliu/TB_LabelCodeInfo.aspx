<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_LabelCodeInfo.aspx.cs" Inherits="Admin_TB_LabelCodeInfo" %>
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
                <div class="topitem">
                        <asp:DropDownList ID="DDLField" runat="server">
                                             <asp:ListItem Value="startfournum">前四位</asp:ListItem>
                                             <asp:ListItem Value="tablenameinfo">表名</asp:ListItem>
                                             <asp:ListItem Value="startvalue">开始号码</asp:ListItem>
                                             <asp:ListItem Value="endvalue">结束号码</asp:ListItem>
                                             <asp:ListItem Value="rowcout">行数</asp:ListItem>
                                             <asp:ListItem Value="remarks">备注</asp:ListItem>
                                         </asp:DropDownList> </div>
                        <div class="topitem">
                           包含： <input id="inputSearchKeyword" type="text" runat="server" placeholder="请输入查找内容"  class="inputsearch" /> 
                            <asp:Button ID="BtnSearch0" runat="server" Text="查找" CssClass=" btn btn-warning btnyd " onclick="BtnSearch0_Click" />
                </div>
               </div>
                <div style="overflow-x:auto"><asp:GridView ID="GridView1" EnableViewState="False" runat="server"  Width="100%" AllowPaging="False" AutoGenerateColumns="False" DataKeyNames="ID"
                              onpageindexchanging="GridView1_PageIndexChanging"   onrowcancelingedit="GridView1_RowCancelingEdit"  onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing"  onrowupdating="GridView1_RowUpdating" onrowdatabound="GridView1_RowDataBound" CssClass="GridViewStyle" PageSize="18" >
                    <Columns>
                        <asp:TemplateField HeaderText="前四位">                   
                            <ItemTemplate>
                                <asp:Label ID="Labelstartfournum" runat="server" Text='<%# Bind("startfournum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="表名">             
                            <ItemTemplate>
                                <asp:Label ID="Labeltablenameinfo" runat="server" Text='<%# Bind("tablenameinfo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="开始号码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtstartvalue" runat="server"  Text='<%# Bind("startvalue") %>'  MaxLength="12"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Labelstartvalue" runat="server" Text='<%# Bind("startvalue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="结束号码">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtendvalue" runat="server" Text='<%# Bind("endvalue") %>'  MaxLength="12"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Labelendvalue" runat="server" Text='<%# Bind("endvalue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="套数">                    
                            <ItemTemplate>
                                <asp:Label ID="Labeltaozongshu" runat="server" Text='<%# Bind("taozongshu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="箱数">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtxiangnum" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("xiangnum") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Labelxiangnum" runat="server" Text='<%# Bind("xiangnum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="瓶数">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtpingnum" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("pingnum") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Labelpingnum" runat="server" Text='<%# Bind("pingnum") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="偏移量">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtpianyiliang" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" Text='<%# Bind("pianyiliang") %>' MaxLength="4"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Labelpianyiliang" runat="server" Text='<%# Bind("pianyiliang") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="记录数">                    
                            <ItemTemplate>
                                <asp:Label ID="Labelrowcout" runat="server" Text='<%# Bind("rowcout") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="备注">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtremarks" runat="server"  Text='<%# Bind("remarks") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Labelremarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>              
                        <asp:CommandField ShowEditButton="True" EditText="删除" />
                    </Columns>
                    <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <RowStyle CssClass="GridViewRowStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                    <webdiyer:AspNetPager ID="AspNetPager1"  ShowFirstLast="True" runat="server" CssClass="GridViewPagerStyle"  PageSize="20"   NextPageText="下一页" PrevPageText="上一页"  PageIndexBoxType="DropDownList" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" FirstPageText="首页" LastPageText="尾页" NumericButtonCount="5" OnPageChanging="AspNetPager1_PageChanging"   CustomInfoHTML="" CustomInfoSectionWidth=""></webdiyer:AspNetPager>

            </div>
        </form>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script> 
        <script type="text/javascript" src="../include/js/UploadImage.js"></script>
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script> 
    </body>
</html>