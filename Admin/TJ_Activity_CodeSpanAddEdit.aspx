<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_Activity_CodeSpanAddEdit.aspx.cs" Inherits="Admin_TJ_Activity_CodeSpanAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_Activity_CodeSpan</title>
    <link href="../include/windows.css" rel="stylesheet" />
    <link href="../include/easyui.css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="editpageback">
                    <div class="toptitle">其他范围限定</div>
                    <table class="gridtable">
                        <tr>
                            <th>活动</th>
                            <td>
                                <input id="inputacid" runat="server" type="text" readonly="readonly" maxlength="4" class="p80" onafterpaste="if(isNaN(value))execCommand('undo')" onkeyup="if(isNaN(value))execCommand('undo')" />
                            </td>
                        </tr>
                        <tr>
                            <th>出厂时间</th>
                            <td>
                                <div style="display: inline-block; width: 100%;"><input id="inputfhsdate" runat="server" placeholder="不限" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3" maxlength="16" /><span class="discriptionspan">至</span><input id="inputfhedate" placeholder="不限" runat="server" type="text" onfocus="WdatePicker({isShowClear:false,readOnly:true})" class="length3" maxlength="16" /><asp:CheckBox ID="CheckBox_AnyFHDate" runat="server" Text="不限" AutoPostBack="True" OnCheckedChanged="CheckBox_AnyFHDate_CheckedChanged" /></div>
                            </td>
                        </tr>
                        <tr runat="server" id="notswm">
                            <th>订单编号</th>
                            <td>
                                <div style="display: inline-block"><asp:DropDownList ID="ddl_dingdancode" runat="server" DataTextField="KhDDH" DataValueField="KhDDH">
                                        <asp:ListItem Value="0">不限</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button runat="server" CssClass="btn btn-info btnyd" Text="刷新" OnClick="Btn_refresh_Click" /></div> 
                            </td>
                        </tr> 
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputremarks" runat="server" type="text" class="p80" maxlength="25" />
                            </td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="确定" /></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="HF_CMD" runat="server" /> 
        <asp:HiddenField runat="server" ID="hf_acid"/>  
        <input id="inputstartfahuodate" type="hidden" runat="server"/>
        <input id="inputendfahuodate" type="hidden" runat="server"/>
        <script src="../include/js/jquery.min.js" type="text/javascript"></script>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script> 
        <script src="../include/js/jquery.easyui.min.js" type="text/javascript"></script>  
        <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
    </form>
</body>
</html>
