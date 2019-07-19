<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductJiageEdit.aspx.cs" Inherits="Admin_wuliu_ProductJiageEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta name="viewport" content="width=device-width,initial-scale=1" />
        <title></title>
       <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
        <div class="editpageback">
           <table class="gridtable">
               <tr><th>产品</th><td><asp:Label ID="lab_productname" runat="server"></asp:Label></td></tr><tr><th>价格：</th><td>￥<asp:TextBox ID="txt_Price" runat="server" CssClass="input5"></asp:TextBox>
                                                                                                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ErrorMessage="请正确输入数字！" ValidationExpression="^[0-9]+(.[0-9]{2})?$" ControlToValidate="txt_Price"></asp:RegularExpressionValidator>
                                                                                                                               </td></tr>
                                                                                                                                                            <%--    <asp:Button ID="Button_OK" runat="server" CssClass="inputbutton" OnClick="Button_OK_Click" Text="确 定" />--%>
                              </table>
              <div class="bottomdivbutton">
                        <asp:Button ID="Button_OK" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button_OK_Click" Text="确 定" /></div>
            </div>
            <asp:HiddenField ID="HF_ProductID" runat="server" />
        </form>
         <script src="../../include/js/UploadImage.js"></script>
    </body>
</html>