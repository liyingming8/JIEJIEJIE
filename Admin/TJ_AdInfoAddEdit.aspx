<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_AdInfoAddEdit.aspx.cs" Inherits="Admin_TJ_AdInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_AdInfo</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <table class="gridtable">            
                    <tr><th>名称</th><td><input id="inputADName" runat="server" type="text" maxlength="25"/></td> </tr>
                    <tr><th>媒体类别</th><td>
                                         <asp:DropDownList ID="ComboBox_MediaType" runat="server">
                                         </asp:DropDownList>
                                     </td> </tr>
                    <tr>
                        <th>宽度</th>
                        <td><input id="inputMWidth" runat="server"  type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="display:inline-block" maxlength="4"/>px</td></tr>
                    <tr>
                        <th>高度</th>
                        <td><input id="inputMHeight" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="display:inline-block" maxlength="4"/>px</td> </tr>
                    <tr>
                        <th>所属模块</th>
                        <td><asp:DropDownList ID="ComboBox_Site" runat="server">
                                         </asp:DropDownList>
                        </td> </tr>
                    <tr>
                        <th>有效</th>
                        <td>
                            <asp:CheckBox ID="CKB_IsActive" runat="server" />
                        </td>
                    </tr>
                </table> 
                <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
            </ContentTemplate>
                </asp:UpdatePanel> 
      </div>
            <asp:HiddenField ID="HF_CMD" runat="server" /><asp:HiddenField ID="HF_ID" runat="server" /> 
        </form>
         <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>