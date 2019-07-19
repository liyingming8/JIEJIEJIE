<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Role_PackageAddEdit.aspx.cs" Inherits="Admin_TJ_Role_PackageAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_Role_Package</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="toptitle">功能包编辑</div>
                <table class="gridtable">
                    <tr>
                        <th>功能描述</th>
                        <td>
                            <input id="inputrpackage" runat="server" type="text" class="p80" maxlength="25" /></td>
                    </tr>
                    <tr>
                        <th>功能模块</th>
                        <td>
                            <asp:Literal ID="Literalridstring" runat="server"></asp:Literal>
                        </td>
                    </tr>
                     <tr>
                        <th>显示顺序</th>
                        <td>
                            <input id="inputshoworder" runat="server" class="p10" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /> 
                        </td>
                    </tr>
                    <tr>
                        <th>备注</th>
                        <td>
                            <input id="inputremarks" runat="server" class="p80" type="text" maxlength="25" /></td>
                    </tr>
                    <tr>
                        <th>价格</th>
                        <td>
                            <input id="inputpricevalue" runat="server" class="p10" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="11" />分/枚</td>
                    </tr>
                   <%-- <tr>
                        <th>折扣模式</th>
                        <td>
                            <input id="inputpricemodel" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td>
                    </tr>--%>
                </table>
                <div class="bottomdivbutton">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js"></script>
    </form>
</body>
</html>
