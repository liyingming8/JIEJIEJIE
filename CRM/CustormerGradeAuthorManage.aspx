<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustormerGradeAuthorManage.aspx.cs" Inherits="CRM_CustormerGradeAuthorManage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>    
        <link href="../include/windows.css" rel="stylesheet" />
        <style type="text/css">
            table.gridtable td {
                border: none !important;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server"> 
            <table class="gridtable" style="width: 100%;border: none;">
                <tr>
                    <td>
                        <asp:CheckBoxList ID="ckblist_customergrade" DataValueField="id" DataTextField="gradename" runat="server" RepeatDirection="Horizontal" BorderStyle="None" BorderWidth="0px" CellPadding="0" CellSpacing="0" RepeatColumns="3">
                        </asp:CheckBoxList> 
                    </td>
                </tr>   
            </table> 
            <div class="bottomdivbutton"><asp:Button ID="Button_OK" runat="server" CssClass="btn btn-warning btnyd" onclick="Button_OK_Click" Text="添加" /></div>
            <asp:HiddenField ID="HF_GID" runat="server" /><asp:HiddenField ID="HF_AuthorGradeIDString" runat="server" /> 
        </form>
    </body>
</html>