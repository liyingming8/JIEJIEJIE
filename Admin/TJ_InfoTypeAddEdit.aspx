<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_InfoTypeAddEdit.aspx.cs" Inherits="Admin_TJ_InfoTypeAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TJ_InfoType</title>  
        <link href="../include/windows.css" rel="stylesheet" /> 
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager> 
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
 <ContentTemplate>  
                <table class="gridtable">
                    <tr><th>父级类别</th><td>
                                         <asp:DropDownList ID="ComboBox_ParentID" runat="server">
                                         </asp:DropDownList>
                                     </td> </tr>
                    <tr><th>类别名称</th><td><input id="inputTypeName" runat="server" type="text" maxlength="25" /></td></tr>
                    <tr><th>备注</th><td><input id="inputRemarks" runat="server" type="text" maxlength="25" /></td> </tr>
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