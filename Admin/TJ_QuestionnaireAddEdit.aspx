<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_QuestionnaireAddEdit.aspx.cs" Inherits="Admin_TJ_QuestionnaireAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_Questionnaire</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">
                <table class="gridtable">
                    <tr><th>id</th><td><input id="inputid" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>Title</th><td><input id="inputTitle" runat="server" type="text" maxlength="500" /></td></tr>
                    <tr><th>StartTime</th><td><input id="inputStartTime" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>EndTime</th><td><input id="inputEndTime" runat="server" type="text" maxlength="8" /></td><td></td></tr>
                    <tr><th>CreatTime</th><td><input id="inputCreatTime" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>Compid</th><td><input id="inputCompid" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="100" /></td></tr>
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