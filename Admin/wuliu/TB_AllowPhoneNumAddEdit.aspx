<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_AllowPhoneNumAddEdit.aspx.cs" Inherits="Admin_TB_AllowPhoneNumAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_AllowPhoneNum</title>
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
           <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </asp:ScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>电话号码</th>
                            <td>
                                <input id="inputPhone_Num" runat="server" type="text" maxlength="20" /></td>
                        </tr>
                        <tr>
                            <th>名称</th>
                            <td>
                                <input id="inputMaster" runat="server" type="text" maxlength="20" /></td>
                        </tr>
                        <tr>
                            <th>是否授权</th>
                            <td>
                                <asp:RadioButton ID="RadioButtonok" runat="server" GroupName="a" Text="是" /><asp:RadioButton ID="RadioButtonno" runat="server" GroupName="a" Text="否" /></td>
                        </tr>

                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" CssClass="btn btn-warning btnyd" runat="server" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_CMD" runat="server" />
    </form>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
