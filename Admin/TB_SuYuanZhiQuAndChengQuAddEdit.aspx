<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiQuAndChengQuAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanZhiQuAndChengQuAddEdit" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>原曲含量信息编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>成曲批次</th>
                            <td>
                                <asp:Label runat="server" ID="labelCQID"></asp:Label></td>
                        </tr>
                        <tr>
                            <th>原曲批次</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxQLID" DataTextField="ZhiQuPC" DataValueField="ID" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>含量</th>
                            <td>
                                <input id="inputPercentValue"  runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="display:inline-block" maxlength="11" />%</td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks"  runat="server" type="text" maxlength="100" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField runat="server" ID="HF_CQID" />
    </form>
     <script src="../include/js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
     <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
