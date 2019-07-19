<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UploadMediaInfoAddEdit.aspx.cs" Inherits="Admin_TJ_UploadMediaInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_UploadMediaInfo</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <table class="gridtable">
                    <tr><th>UPLID</th><td><input id="inputUPLID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>UID</th><td><input id="inputUID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>UploadDate</th><td><input id="inputUploadDate" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>PhysicalPath</th><td><input id="inputPhysicalPath" runat="server" type="text" maxlength="150" /></td></tr>
                    <tr><th>LinkURL</th><td><input id="inputLinkURL" runat="server" type="text" maxlength="150" /></td></tr>
                    <tr><th>MediaType</th><td><input id="inputMediaType" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>Introductions</th><td><input id="inputIntroductions" runat="server" type="text" maxlength="150" /></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>Show</th><td><input id="inputShow" runat="server" type="text" maxlength="0" /></td></tr>
                </table>　
                 <div class="bottomdivbutton"><asp:Button ID="Button1" CssClass="btn btn-warning btnyd" runat="server" onclick="Button1_Click" Text="添加" /></div>
        
            </div>
               
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
          <script src="../include/js/UploadImage.js" type="text/javascript"></script>
        </body>
</html>