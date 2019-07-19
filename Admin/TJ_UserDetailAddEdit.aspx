<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UserDetailAddEdit.aspx.cs" Inherits="Admin_TJ_UserDetailAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_UserDetail</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
           <div class="editpageback">
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> TJ_UserDetail</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="TJ_UserDetail.aspx"><img title="返回" src="images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table>
                <table class="gridtable">
                    <tr><th>UserDetailID</th><td><input id="inputUserDetailID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>UserID</th><td><input id="inputUserID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>CTID</th><td><input id="inputCTID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>Birthday</th><td><input id="inputBirthday" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>Address</th><td><input id="inputAddress" runat="server" type="text" maxlength="40" /></td></tr>
                    <tr><th>IdentificationCode</th><td><input id="inputIdentificationCode" runat="server" type="text" maxlength="18" /></td></tr>
                    <tr><th>PhoneNumber</th><td><input id="inputPhoneNumber" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>EMail</th><td><input id="inputEMail" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>QQ</th><td><input id="inputQQ" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>WeiXinNumber</th><td><input id="inputWeiXinNumber" runat="server" type="text" maxlength="15" /></td><td></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td></tr>
                </table>
                     <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>