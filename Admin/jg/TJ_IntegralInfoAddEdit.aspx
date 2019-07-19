<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_IntegralInfoAddEdit.aspx.cs" Inherits="Admin_jg_TJ_IntegralInfoAddEdit" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_IntegralInfo</title>
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="../images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> 
                                                         活动项目编辑</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="TJ_Integral.aspx"><img title="返回" src="../images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table> 
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr><td>活动名称</td><td>
                                                 <input ID="inputIntegralName" runat="server" type="text" size="40" 
                                                        disabled="disabled" readonly="readonly" /></td><td>
                                                                                                           ;</td></tr>
                            <tr>
                                <td>
                                    活动产品</td>
                                <td>
                                    <asp:CheckBoxList ID="CHKList_Goods" runat="server" 
                                                      RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr><td>积分项目</td><td>
                                                 <asp:DropDownList ID="ddl_IntegelItems" runat="server" DataTextField="ItemName" 
                                                               DataValueField="ITITID">
                                                 </asp:DropDownList>
                                             </td><td></td></tr>
                            <tr><td>奖励积分</td><td><input id="inputIntegralReword" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20" /></td><td>
                                                                                                                                                                                                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                                                                                                                                                                                                                   ControlToValidate="inputIntegralReword" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                                                                                                                                                                                   </td></tr> 
                            <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="20" 
                                                      size="80" /></td><td></td></tr>
                            <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> 
                                                                                                                                               ;</td></tr>
                            <tr>
                                <td>
                                    ;</td>
                                <td colspan="2">
                                    <asp:GridView ID="GRV_IntegralItems" runat="server" AutoGenerateColumns="False" OnRowDeleting="GRV_IntegralItems_RowDeleting"  OnRowDataBound="GRV_IntegralItems_RowDataBound" DataKeyNames="ITGRID"
                                                  OnRowCancelingEdit="GRV_IntegralItems_RowCancelingEdit" OnRowEditing="GRV_IntegralItems_RowEditing"  CssClass="GridViewStyle">
                                        <Columns>    
                                            <asp:BoundField DataField="ITGID" HeaderText="ID" />
                                            <asp:TemplateField HeaderText="活动名称">                               
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_IntegalName" Text='<%# GetIntegralName(Eval("ITGRID").ToString()) %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="活动产品">                               
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_Goods" Text='<%# GetGoodNameByIDString(Eval("GoodID").ToString()) %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="积分项目">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_ItemName" runat="server"  Text='<%# GetIntegralItemName(Eval("IntegralItemID").ToString()) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="积分" DataField="IntegralReword" />
                                            <asp:BoundField HeaderText="备注" DataField="Remarks" />
                                            <asp:HyperLinkField DataNavigateUrlFields="ITGID,ITGRID"                                
                                                                DataNavigateUrlFormatString="TJ_IntegralInfoAddEdit.aspx?cmd=edit&ITGID={0}&ITID={1}"
                                                                Text="编辑" />
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
                                        <EmptyDataTemplate>尚未检索到数据</EmptyDataTemplate>
                                        <FooterStyle CssClass="GridViewFooterStyle" />
                                        <RowStyle CssClass="GridViewRowStyle" />
                                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" /><PagerStyle CssClass="GridViewPagerStyle" Font-Bold="True" Font-Size="12px" ForeColor="White" /><AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" /><HeaderStyle CssClass="GridViewHeaderStyle" /></asp:GridView></div>
                                    <asp:HiddenField ID="HF_CMD" runat="server" />
                                    <asp:HiddenField ID="HF_ITGID" runat="server" />
                                    <asp:HiddenField ID="HF_ITID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
        </form>
    </body>
</html>