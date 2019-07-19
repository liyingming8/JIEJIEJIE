<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SmallBuJiangAddEdit.aspx.cs" Inherits="Admin_TB_SmallBuJiangAddEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>TB_SmallBuJiang</title>
          <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../include/windows.css" rel="stylesheet" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="editpageback"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <table class="gridtable">
                    <tr>
                        <th>奖项</th>
                        <td>
                            <asp:DropDownList ID="ComboBox_JX" AppendDataBoundItems="true" runat="server" DataTextField="JxName" DataValueField="JxID">
                                <asp:ListItem Text="指定奖项..." Value="0" Selected="True"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th>设奖数量</th>
                        <td>
                            <input id="inputnum" runat="server" type="text" /></td>
                    </tr>
                    <tr>
                        <th>开始号码</th>
                        <td>
                            <input id="inputStartlabelcode" runat="server" type="text"/></td>
                    </tr>
                    <tr>
                        <th>结束号码</th>
                        <td>
                            <asp:TextBox ID="inputEndlabelcode" runat="server" AutoPostBack="True" OnTextChanged="inputEndlabelcode_TextChanged"></asp:TextBox>    </td>
                    </tr>
                    <tr>
                        <th>可布奖数量</th>
                        <td>
                            <input id="inputcount" runat="server" type="text" readonly="readonly" /></td>
                    </tr>
                    <tr>
                        <th>备注</th>
                        <td>
                            <input id="inputRemarks" runat="server" type="text" /></td>
                    </tr>
                </table> 
                 <div class="bottomdivbutton"><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" CssClass="btn btn-warning btnyd"/></div>
                            <asp:HiddenField ID="HF_CMD" runat="server" />
                            <asp:HiddenField ID="HF_ID" runat="server" />
            </div> 
        </form>
          <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>