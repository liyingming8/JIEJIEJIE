<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_CompAgentInfoAddEdit.aspx.cs" Inherits="Admin_TB_CompAgentInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_CompAgentInfo</title>
          <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">  
                <table class="gridtable">
                    <tr><th>CAID</th><td><input id="inputCAID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>CompID</th><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>AgentID</th><td><input id="inputAgentID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>CreateDate</th><td><input id="inputCreateDate" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>UserID</th><td><input id="inputUserID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td><td></td></tr>
                    <tr><th>IsActive</th><td><input id="inputIsActive" runat="server" type="text" maxlength="0" /></td></tr>
                    <tr><th>Middleman</th><td><input id="inputMiddleman" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>AllowArea</th><td><input id="inputAllowArea" runat="server" type="text" maxlength="100" /></td></tr>
                    <tr><th>PhoneNumber</th><td><input id="inputPhoneNumber" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td></tr>
                   
                </table>
               <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>