<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompTypeIDAuthorManage.aspx.cs" Inherits="Admin_CompTypeIDAuthorManage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>    
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="include/MainFrame.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:DropDownList ID="DDL_Role" runat="server" AutoPostBack="True" DataTextField="RoleName" DataValueField="RID" onselectedindexchanged="DDL_Role_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:HiddenField ID="HF_AuthorCompTypeIDString" runat="server" /> 
            </div>
            <asp:CheckBoxList ID="CheckBoxList_CompanyType" runat="server" DataTextField="CName" DataValueField="CID" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList>
            <asp:Button ID="Button_OK" runat="server" onclick="Button_OK_Click" Text="确定" />
            <asp:HiddenField ID="HF_RID" runat="server" />
        </form>
    </body>
</html>