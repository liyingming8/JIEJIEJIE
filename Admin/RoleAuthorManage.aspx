<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleAuthorManage.aspx.cs" Inherits="Admin_RoleAuthorManage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:DropDownList ID="DDL_Role" runat="server" AutoPostBack="True" DataTextField="RoleName" DataValueField="RID" onselectedindexchanged="DDL_Role_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:HiddenField ID="HF_AuthorRoleIDString" runat="server" /> 
            </div>
            <asp:CheckBoxList ID="CheckBoxList_RoleInfo" runat="server" DataTextField="RoleName" DataValueField="RID" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList>
             <asp:Button ID="Button_OK" runat="server" onclick="Button_OK_Click" Text="确定" />
            <asp:HiddenField ID="HF_RID" runat="server" />
        </form>
    </body>
</html>