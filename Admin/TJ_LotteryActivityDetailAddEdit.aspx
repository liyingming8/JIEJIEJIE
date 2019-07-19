<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_LotteryActivityDetailAddEdit.aspx.cs" Inherits="Admin_TJ_LotteryActivityDetailAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_LotteryActivityDetail</title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <table class="gridtable">
                    <tr><th>LADID</th><td><input id="inputLADID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>LAID</th><td><input id="inputLAID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>GradeName</th><td><input id="inputGradeName" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>GradeID</th><td><input id="inputGradeID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>Numbers</th><td><input id="inputNumbers" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>AwardName</th><td><input id="inputAwardName" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>AwardPictrureURL</th><td><input id="inputAwardPictrureURL" runat="server" type="text" maxlength="50" /></td></tr>
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