<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_VanInfoAddEdit.aspx.cs" Inherits="Admin_TJ_VanInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_VanInfo</title>
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
                                <tr><td align="left"><span style="font-size: 12px; font-weight: bold; color: #3666AA"><img src="images/icon.gif" align="middle" style="border-width: 0px; margin-top: -5px;" /> TJ_VanInfo</span></td>
                                    <td align="center"><table align="center" id="table3"><tr valign="top" align="center"><td width="80"><a href="TJ_VanInfo.aspx"><img title="返回" src="images/back.png" border="0"></a></td><td width="100"></td><td width="100"></td><td width="100"></td></tr>
                                                       </table></td></tr></table></td></tr></table> 
                  <table class="gridtable">
                    <tr><th>VanID</th><td><input id="inputVanID" runat="server" type="text" maxlength="2" /></td></tr>
                    <tr><th>VanTypeID</th><td><input id="inputVanTypeID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>VanBrandID</th><td><input id="inputVanBrandID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>VanCarryAbID</th><td><input id="inputVanCarryAbID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>VanSizeID</th><td><input id="inputVanSizeID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>VanMaterID</th><td><input id="inputVanMaterID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>DriverID</th><td><input id="inputDriverID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>VanCertifID</th><td><input id="inputVanCertifID" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" /></td></tr>
                    <tr><th>NumberPlate</th><td><input id="inputNumberPlate" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>VanPicture</th><td><input id="inputVanPicture" runat="server" type="text" maxlength="100" /></td></tr>
                    <tr><th>VanIntructions</th><td><input id="inputVanIntructions" runat="server" type="text" maxlength="50" /></td></tr>
                    <tr><th>VehicleLicenseCode</th><td><input id="inputVehicleLicenseCode" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>VehicleLicensePicture</th><td><input id="inputVehicleLicensePicture" runat="server" type="text" maxlength="50" /></td></tr>
                    <tr><th>OperationCertificateCode</th><td><input id="inputOperationCertificateCode" runat="server" type="text" maxlength="15" /></td></tr>
                    <tr><th>OperationCertificatePicture</th><td><input id="inputOperationCertificatePicture" runat="server" type="text" maxlength="50" /></td></tr>
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