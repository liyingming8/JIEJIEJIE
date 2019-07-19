<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tj_crm_rewardtypeAddEdit.aspx.cs" Inherits="CRM_tj_crm_rewardtypeAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>tj_crm_rewardtype</title>
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="editpageback">
                    <div class="toptitle">奖励类型</div>
                    <table class="gridtable">
                        <tr>
                            <th>类型</th>
                            <td>
                                <input id="inputname" runat="server" class="p80" type="text" /></td>
                            <td></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <script src="../js/jquery-1.7.1.js"></script>
        <script src="../include/js/UploadImage.js"></script>
    </form>
</body>
</html>
