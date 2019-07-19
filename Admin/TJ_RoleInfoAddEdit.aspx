<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RoleInfoAddEdit.aspx.cs" Inherits="Admin_TJ_RoleInfoAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_RoleInfo</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
               <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            <div class="editpageback">  
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate> 
                        <table class="gridtable">          
                            <tr><th>角色名称</th><td><input id="inputRoleName" runat="server" class="length8" type="text" maxlength="20" /></td></tr>
                            <tr><th>企业级权限</th><td>
                                <asp:CheckBox ID="ckb_comp_grade" runat="server" />
                                </td></tr> 
                            <tr>
                                <th>备注</th>
                                <td>
                                    <input id="inputRemark" runat="server" type="text" maxlength="25" />
                                </td>
                            </tr>
                        </table> 
                        <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" onclick="Button1_Click" Text="添加" /></div>
                    </ContentTemplate>
                </asp:UpdatePanel> 
            </div>
             <asp:HiddenField ID="HF_CMD" runat="server" />
             <asp:HiddenField ID="HF_ID" runat="server" />
        </form>
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>