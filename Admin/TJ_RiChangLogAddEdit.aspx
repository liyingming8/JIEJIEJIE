<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_RiChangLogAddEdit.aspx.cs" Inherits="Admin_TJ_RiChangLogAddEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_RiChangLog</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
        <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback">
            <table class="gridtable"> 
                <tr>
                    <th>标题</th>
                    <td>
                        <input id="inputBiaoTi" runat="server" type="text" /><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputBiaoTi" ErrorMessage="标题不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>内容</th>
                    <td>
                        <asp:TextBox ID="txtcontent" runat="server" Height="196px" TextMode="MultiLine" Width="434px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcontent" ErrorMessage="内容不能为空"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <th>提交时间</th>
                    <td>
                        <asp:TextBox ID="TijiaoTime" runat="server" Enabled="true"></asp:TextBox><cc2:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TijiaoTime" Format="yyyy-MM-dd"></cc2:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
                        </asp:ScriptManager>
                    </td>
                </tr>
            </table>
                  <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" />
                    </div>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
      <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    </body>
</html>
