<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DepartMentAddEdit.aspx.cs" Inherits="Admin_TJ_DepartMentAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_DepartMent</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="toptitle">组织信息编辑</div>
                <table class="gridtable">
                    <tr>
                        <th>所属单位</th>
                        <td>
                            <input type="hidden" id="hd_compid" runat="server" /><input id="inputcompid" runat="server" type="text" class="p60" readonly="readonly" maxlength="4" /></td>
                    </tr>
                    <tr>
                        <th>父级</th>
                        <td>
                            <asp:DropDownList ID="ddl_parentid" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>部门/单位</th>
                        <td>
                            <input id="inputdepartment" runat="server" type="text" class="p60" maxlength="25" /></td>
                    </tr>
                    <tr>
                        <th>负责人</th>
                        <td>
                            <input id="inputleadername" runat="server" type="text" maxlength="10" /></td>
                    </tr>
                    <tr>
                        <th>备注</th>
                        <td>
                            <input id="inputremarks" runat="server" class="p80" type="text" maxlength="25" /></td>
                    </tr>
                    <tr>
                        <th>最近更新</th>
                        <td>
                            <asp:Label ID="Label_creatuserid" runat="server"></asp:Label><asp:Label ID="Labelcreatetm" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div class="bottomdivbutton">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js"></script>
        <script src="../include/js/jquery.easyui.min.js"></script>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
</body>
</html>
