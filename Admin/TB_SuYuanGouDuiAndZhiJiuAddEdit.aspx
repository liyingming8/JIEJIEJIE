﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanGouDuiAndZhiJiuAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanGouDuiAndZhiJiuAddEdit" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanGouDuiAndZhiJiu</title>
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
                            <th>勾调批次</th>
                            <td>
                                <asp:Label ID="LabelGouDuiID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>制酒批次</th>
                            <td>
                                <asp:DropDownList ID="ComboBoxZhiJiuID" runat="server" DataTextField="ZJPC" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>含量</th>
                            <td>
                                <input id="inputPercentValue" runat="server" class="length1" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="display:inline-block;width:60px" maxlength="11" />%</td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" class="length10" runat="server" type="text" maxlength="100" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" /> 
        <asp:HiddenField ID="HF_GDID" runat="server" /> 
    </form>
    <script src="../include/js/jquery-1.7.1.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
</body>
</html>