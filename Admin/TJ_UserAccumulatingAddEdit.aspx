<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_UserAccumulatingAddEdit.aspx.cs" Inherits="Admin_TJ_UserAccumulatingAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_UserAccumulating</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
  
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback"> 
            <table class="gridtable">
                <tr>
                    <th>UACID</th>
                    <td>
                        <input id="inputUACID" runat="server" type="text" maxlength="2" /></td>
                  
                </tr>
                <tr>
                    <th>UID</th>
                    <td>
                        <input id="inputUID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td>
                   
                </tr>
                <tr>
                    <th>COMPID</th>
                    <td>
                        <input id="inputCOMPID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td>
                </tr>
                <tr>
                    <th>Accumulating</th>
                    <td>
                        <input id="inputAccumulating" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="20" /></td>
                </tr> 
            </table>
                  <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
        </div>
         <asp:HiddenField ID="HF_CMD" runat="server" />
         <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
