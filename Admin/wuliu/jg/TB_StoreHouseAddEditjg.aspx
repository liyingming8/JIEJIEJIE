<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_StoreHouseAddEditjg.aspx.cs" Inherits="Admin_TB_StoreHouseAddEditjg" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_StoreHouse</title>
        <link href="../../../include/MasterPage.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <table class="gridtable">
                    <tr><td>库房编码</td><td><input id="inputStoreHouseCode" runat="server" type="text" maxlength="10" disabled="True" /></td><td></td></tr>
                    <tr><td>库房名称</td><td><input id="inputStoreHouseName" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><td>城市</td><td>
                                       <asp:DropDownList ID="ComboBox_CID" runat="server">
                                       </asp:DropDownList>
                                   </td><td></td></tr>
                    <tr><td>地址</td><td><input id="inputAddressString" runat="server" type="text" maxlength="100" /></td><td></td></tr>
                    <tr><td>联系人</td><td><input id="inputContractor" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><td>电话</td><td><input id="inputTelPhoneNumber" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><td>手机</td><td><input id="inputMobilePhone" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><td>备注</td><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td><td></td></tr>
                    <tr><td></td><td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添加" /></td><td> <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /></td></tr>
                </table>
                <br />
            </div>
        </form>
        <script src="../../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>