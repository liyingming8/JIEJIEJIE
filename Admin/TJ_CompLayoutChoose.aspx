<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompLayoutChoose.aspx.cs" Inherits="Admin_TJ_CompLayoutChoose" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_CompADInfo</title>
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
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> 模版选择</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table> 
                <table>
                    <tr><td></td><td colspan="2">
                                           <asp:DataList ID="DataList_CSS" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
                                               <ItemTemplate>
                                                   <table >
                                                       <tr>
                                                           <td align="center">
                                                               <asp:Image ID="ImageCSS" ImageUrl='<%# Eval("PicURL").ToString() %>' runat="server" />
                                                           </td>
                                                       </tr>
                                                       <tr>
                                                           <td align="center">
                                                               <asp:RadioButton ID="RB_Choose" Text='<%# Eval("CSSName") %>' runat="server" />
                                                               <asp:HiddenField ID="HF_CSID" runat="server" Value='<%# Eval("CSID").ToString().Trim() %>' />
                                                           </td>
                                                       </tr>
                                                   </table>
                                               </ItemTemplate>
                                           </asp:DataList>
                                       </td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td > <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table> 
            </div>
        </form>
    </body>
</html>