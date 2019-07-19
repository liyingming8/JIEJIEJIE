﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemAuthorManage.aspx.cs" Inherits="Admin_SystemAuthorManage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>    
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="../include/MainFrame.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:DropDownList ID="DDL_Role" runat="server" AutoPostBack="True" DataTextField="RoleName" DataValueField="RID" onselectedindexchanged="DDL_Role_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:HiddenField ID="HF_AuthorMenuString" runat="server" /> 
            </div>
            <table style="width: 100%">
                <asp:Repeater ID="rptSystemMenus" runat="server" 
                              onitemdatabound="rptSystemMenus_ItemDataBound" >
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="labelParentPageName" Text='<%# Eval("PageName") %>' Font-Bold="true" runat="server"></asp:Label>                
                                <asp:HiddenField  ID="HF_Parent" Value='<%# Eval("SiteID") %>' runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CheckList_SubMenus" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" RepeatLayout="Flow" RepeatColumns="6">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
     
                <tr>
                    <td>
                        <asp:Button ID="Button_OK" runat="server" onclick="Button_OK_Click" Text="确定" />
                    </td>
                </tr>     
            </table> 
            <asp:HiddenField ID="HF_RID" runat="server" />
            <asp:HiddenField runat="server" ID="HF_RPID"/>
        </form>
    </body>
</html>