<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_RuKuInfoAddEdit.aspx.cs" Inherits="Admin_TB_RuKuInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_RuKuInfo</title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />  
    <link href="../../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
              <div class="editpageback"> 
           <table class="gridtable">
                    <tr><th>RKID</th><td><input id="inputRKID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>RKPiCi</th><td><input id="inputRKPiCi" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>RKDate</th><td><input id="inputRKDate" runat="server" type="text" maxlength="8" /></td></tr>
                    <tr><th>StoreHouseID</th><td><input id="inputStoreHouseID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>RuKuUserID</th><td><input id="inputRuKuUserID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>TableNameInfo</th><td><input id="inputTableNameInfo" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>RKKey</th><td><input id="inputRKKey" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>Remarks</th><td><input id="inputRemarks" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>CompID</th><td><input id="inputCompID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr> 
                </table>　
                   <div class="bottomdivbutton"> <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="添 加" CssClass="btn btn-warning btnyd"  /> </div> 
            </div>
              <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>