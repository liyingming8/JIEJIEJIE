<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SmallBuJiangdelte.aspx.cs" Inherits="Admin_TB_SmallBuJiangdelte" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SmallBuJiang</title>
         <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table>
                    <tr>
                        <td>开始号码</td>
                        <td>
                            <input id="inputStartlabelcode" runat="server" type="text" maxlength="50" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>结束号码</td>
                        <td>
                            <asp:TextBox ID="inputEndlabelcode" runat="server" AutoPostBack="True" OnTextChanged="inputEndlabelcode_TextChanged"></asp:TextBox>    </td>
                        <td></td>
                    </tr>
               
                    <tr>
                        <td>删除数量</td>
                        <td>
                            <input id="inputcount" runat="server" type="text" readonly="readonly" /></td>
                        <td></td>
                    </tr>
               
                    <tr>
                        <td>备注</td>
                        <td>
                            <input id="inputRemarks" runat="server" type="text" /></td>
                        <td></td>
                    </tr>
              
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="Button1" runat="server"   OnClientClick=" return confirm('确定要删除吗?') " OnClick="Button1_Click" Text="删除" Width="50px" /></td>
                        <td>
                            <asp:HiddenField ID="HF_CMD" runat="server" />
                            <asp:HiddenField ID="HF_ID" runat="server" />
                        </td>
                    </tr>
                </table> 
            </div> 
        </form>
    </body>
</html>