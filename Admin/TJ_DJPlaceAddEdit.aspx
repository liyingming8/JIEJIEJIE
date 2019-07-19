<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DJPlaceAddEdit.aspx.cs" Inherits="Admin_TJ_DJPlaceAddEdit" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_DJPlace</title>
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
                            <th>兑奖点名称</th>
                            <td>
                                <input id="inputDjdName" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th>兑奖帐号</th>
                            <td>
                                <input id="inputDjZH" runat="server" type="text" maxlength="10" /></td>
                        </tr>
                        <tr>
                            <th>验证码</th>
                            <td>
                                <input id="inputYanZM" runat="server" type="text" maxlength="10" /></td>
                        </tr>
                        <tr>
                            <th>奖项名称</th>
                            <td>
                                <input id="inputJxName" runat="server" type="text" maxlength="10" /></td>
                        </tr>
                        <tr>
                            <th>奖项数量</th>
                            <td>
                                <input id="inputJxCount" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td>
                        </tr>
                        <tr>
                            <th>联系人</th>
                            <td>
                                <input id="inputLXname" runat="server" type="text" maxlength="10" /></td>
                        </tr>
                        <tr>
                            <th>联系电话</th>
                            <td>
                                <input id="inputMPhone" runat="server" type="text" maxlength="10" /></td>
                        </tr>
                        <tr>
                            <th>详细地址</th>
                            <td>
                                <input id="inputAddress" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th>兑奖点等级</th>
                            <td>
                                <input id="inputDjGrade" runat="server" type="text" maxlength="10" /></td>
                        </tr>
                        <tr>
                            <th>所在城市</th>
                            <td>
                                <asp:DropDownList ID="Comb_PlaceID" runat="server" DataTextField="CName"
                                    DataValueField="CID">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <%--<tr><td>CompID</td><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>--%>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text" maxlength="25" /></td>
                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <input id="inputDelFlag" runat="server" type="text" maxlength="1" value="1" visible="false" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
