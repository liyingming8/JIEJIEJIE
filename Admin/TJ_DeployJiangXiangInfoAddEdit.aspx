<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_DeployJiangXiangInfoAddEdit.aspx.cs" Inherits="Admin_TJ_DeployJiangXiangInfoAddEdit" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_DeployJiangXiangInfo</title>
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
                            <th>奖项</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_JX" AppendDataBoundItems="true" runat="server" DataTextField="JxName" DataValueField="JxID">
                                    <asp:ListItem Text="指定奖项..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <th>经销商</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_AgentID" AppendDataBoundItems="true" runat="server" DataTextField="CompName" DataValueField="CompID">
                                    <asp:ListItem Text="经销商..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>出库单号</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_ChuKuDanHao" AppendDataBoundItems="true" DataTextField="FHPiCi" DataValueField="FHPiCi" runat="server">
                                    <asp:ListItem Text="指定出库单号..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>产品</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_Prod" AppendDataBoundItems="true" runat="server" DataTextField="Products_Name" DataValueField="Infor_ID">
                                    <asp:ListItem Text="产品..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>兑奖点</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_DJD" AppendDataBoundItems="true" runat="server" DataTextField="CompName" DataValueField="CompID">
                                    <asp:ListItem Text="指定兑奖点..." Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="4">
                                <asp:RadioButtonList ID="RBList_Mode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RBList_Mode_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">按箱</asp:ListItem>
                                    <asp:ListItem Value="2">按比例</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="LabelName" runat="server" Text="每箱数量"></asp:Label>
                            </th>
                            <td colspan="3">
                                <input id="inputPreOderNum" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="input4" /><asp:Label ID="Label_percent" runat="server"></asp:Label>
                                <asp:Label ID="Label_LimitTotalNum" runat="server" Text="布奖总数：" Visible="False"></asp:Label>
                                <input id="inputTotalNum" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="input4" readonly="readonly" />
                                <asp:CheckBox ID="CheckBox_LimitedTotalNum" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_LimitedTotalNum_CheckedChanged" Text="指定数量" />
                            </td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td colspan="4">
                                <input id="inputRemarks" runat="server" type="text" maxlength="25" class="input6" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd" />
                    </div>
                    <asp:HiddenField ID="HF_CMD" runat="server" />
                    <asp:HiddenField ID="HF_ID" runat="server" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
