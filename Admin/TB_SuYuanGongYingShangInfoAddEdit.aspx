<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanGongYingShangInfoAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanGongYingShangInfoAddEdit" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanGongYingShangInfo</title>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <div class="editpageback">
         <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <%--  <tr><th>ID</th><td><input id="inputID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>--%>
                        <tr>
                            <th>供应商名称</th>
                            <td>
                                <input id="inputGYSName" runat="server" type="text" /></td>
                        </tr>
                        <tr>
                            <th>省份城市</th>
                            <td>
                                <asp:DropDownList ID="ComboBox_CTID" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>详细地址</th>
                            <td>
                                <input id="inputGYSAddress" runat="server" type="text"  /></td>
                        </tr>
                        <tr>
                            <th>供应类别</th>
                            <td>
                                <asp:CheckBoxList ID="cbl_spID" runat="server" DataTextField="CName" DataValueField="CID" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <th>手机号码</th>
                            <td>
                                <%--<input id="inputCtiyID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" />--%>
                                <input id="inputGYSPhoneNum" runat="server" type="text"  onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" onafterpaste="this.value=this.value.replace(/[^0-9]/g,'')" />
                            </td>
                        </tr>
                        <%--  <tr><th>Compid</th><td><input id="inputCompid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>--%>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server" type="text" /></td>
                        </tr>
                        <%-- <tr><th>备注1</th><td><input id="inputRemarks1" runat="server" type="text" maxlength="250" /></td><td></td></tr>--%>
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
       <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
       <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    
</body>
</html>
