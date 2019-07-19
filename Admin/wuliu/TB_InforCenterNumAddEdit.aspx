<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_InforCenterNumAddEdit.aspx.cs" Inherits="Admin_TB_InforCenterNumAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_InforCenterNum</title>
         <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
       <link href="../../include/windows.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">
                <table class="gridtable">
                    <tr><th>ID</th><td><input id="inputID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>Province</th><td><input id="inputProvince" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>City</th><td><input id="inputCity" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>Server</th><td><input id="inputServer" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>CenterNumber</th><td><input id="inputCenterNumber" runat="server" type="text" maxlength="10" /></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td></tr>
           </table>
                  <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" />
            <asp:HiddenField ID="HF_ID" runat="server" />
              
        </form>
        <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>