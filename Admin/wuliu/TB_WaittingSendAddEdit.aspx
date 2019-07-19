﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_WaittingSendAddEdit.aspx.cs" Inherits="Admin_TB_WaittingSendAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_WaittingSend</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
       
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">
                <table class="user_border" cellspacing="0" cellsadding="0" width="100%" align="center" border="0" id="table1">
                    <tr>
                        <td valign="middle">
                            <table class="user_box" cellspacing="0" cellpadding="5" width="100%" border="0" id="table2">
                                <tr><td align="left"><span style="font-size: 12pt; font-weight: bold; color: #3666AA"><img src="images/icon.gif" align="middle" style="border-width: 0px;" /> TB_WaittingSend</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="javascript:history.go(-1)"><img title="返回" src="images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table>
            
              <table class="gridtable">
                    <tr><th>ID</th><td><input id="inputID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>Phone_Num</th><td><input id="inputPhone_Num" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>Contents</th><td><input id="inputContents" runat="server" type="text" maxlength="100" /></td></tr>
                    <tr><th>Received_Time</th><td><input id="inputReceived_Time" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>State</th><td><input id="inputState" runat="server" type="text" maxlength="5" /></td></tr>
                    <tr><th>MSGCenterNo</th><td><input id="inputMSGCenterNo" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="100" /></td></tr>
                    <tr><th>CompID</th><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                </table>
                 <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>