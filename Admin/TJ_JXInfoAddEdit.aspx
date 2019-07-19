<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_JXInfoAddEdit.aspx.cs" Inherits="Admin_TJ_JXInfoAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_JXInfo</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
                    <table class="gridtable"> 
                        <tr>
                            <th>所属活动</th>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="LotteryActivityName" Width="127px" DataValueField="LAID">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>奖项名称</th>
                            <td>
                                <input id="inputJxName" runat="server" type="text" maxlength="25" /></td>
                        </tr> 
                        <tr>
                            <th>奖品</th>
                            <td>
                                <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true" Width="127px">
                                    <asp:ListItem Value="jf">扫码积分</asp:ListItem>
                                    <asp:ListItem Value="hb">微信红包</asp:ListItem>
                                    <asp:ListItem Value="kq">微信卡券</asp:ListItem>
                                    <asp:ListItem Value="qt">其他</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr> 
                        <tr>
                            <th id="jpje" runat="server">奖品金额</th>
                            <td>
                                <input id="inputJxContent" runat="server" type="text" maxlength="25" class="input6" /></td>
                        </tr>
                        <tr>
                            <th>预设数量</th>
                            <td>
                                <asp:TextBox ID="TextBox_OrderNum" runat="server" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" CssClass="input4"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <th>备注</th>
                            <td>
                                <asp:TextBox ID="inputRemarks" runat="server" CssClass="input6"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" /></div>
                    <asp:HiddenField ID="HF_CMD" runat="server" />
                    <asp:HiddenField ID="HF_ID" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
