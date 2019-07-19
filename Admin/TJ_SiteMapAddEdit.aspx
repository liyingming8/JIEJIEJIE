<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SiteMapAddEdit.aspx.cs" Inherits="Admin_TJ_SiteMapAddEdit" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_SiteMap</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>父级目录</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_ParentSiteID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>目录名称</th>
                            <td>
                                <input id="inputPageName" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th>链接地址</th>
                            <td>
                                <input id="inputLinkPath" runat="server" class="p80" type="text" maxlength="100" /></td>
                        </tr>
                        <tr>
                            <th>所属系统</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_SystemType" runat="server" DataTextField="CName" DataValueField="CID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>显示顺序</th>
                            <td colspan="2">
                                <input id="inputShowOrder" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" runat="server" class="length1" />
                            </td>
                        </tr>
                        <tr>
                            <th>目录LOGO</th>
                            <td colspan="2">
                                 <input id="inputsystemlogo" runat="server" class="p60"/>
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text" maxlength="25" /></td>
                        </tr>

                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         <asp:HiddenField ID="HF_ImageURL" runat="server" /> 
         <asp:HiddenField ID="HF_ID" runat="server" />
         <asp:HiddenField ID="HF_CMD" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
