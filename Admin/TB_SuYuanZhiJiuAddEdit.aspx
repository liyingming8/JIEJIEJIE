<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiJiuAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanZhiJiuAddEdit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanZhiJiu</title>
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
                            <th>制酒批次</th>
                            <td>
                                <input id="inputZJPC" runat="server" type="text" maxlength="100" />
                                </td>
                        </tr>
                        <tr>
                            <th>制酒日期</th>
                            <td>
                                <input id="inputZhiJiuTime" class="date" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" maxlength="16" />
                            </td>
                        </tr>
                        <tr>
                            <th>仓库</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxCangKuID" runat="server" DataTextField="KuFang" DataValueField="ID" RenderMode="ComboBox">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>质检员</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxZhiJianYuanID" runat="server" DataTextField="ZhiJianName" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
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
    </form>
     <script src="../include/js/jquery-1.7.1.js"></script>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
     <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>
