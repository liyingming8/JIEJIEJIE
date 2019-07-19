<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_CompanyGoodsTypesAddEdit.aspx.cs" Inherits="Admin_TJ_CompanyGoodsTypesAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>产品类别编辑</title>
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
                        <tr>
                            <th>产品类别</th>
                            <td>
                                <input id="inputGoodsType" runat="server" type="text" maxlength="25" /></td>
                           <%-- <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="inputGoodsType" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>--%>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemark" runat="server" type="text" maxlength="25" /></td> 
                        </tr>
                    </table>
                      <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
