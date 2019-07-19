<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_BQLabelTHAddEdit.aspx.cs" Inherits="Admin_TB_BQLabelTHAddEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_Products_Type</title>
     <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" /> 
    <link href="../../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server"> 
        <div class="editpageback">
            <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>替换模式 </th>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonList_Mode" runat="server" AutoPostBack="false"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">替换瓶标</asp:ListItem>

                                    <asp:ListItem Value="2">替换箱标</asp:ListItem>

                                </asp:RadioButtonList>
                            </td> 
                        </tr>
                        <tr>
                            <th>单个替换 </th>
                        </tr>
                        <tr>
                            <th>原标签序号</th>
                            <td>
                                <input id="inputCodeOld" runat="server" type="text" maxlength="30" /></td> 
                        </tr>
                        <tr>
                            <th>新标签序号</th>
                            <td>
                                <input id="inputCodeNew" runat="server" type="text" maxlength="25" /></td> 
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputBZ" runat="server" type="text" maxlength="25" /></td>
                        </tr> 
                        <tr>
                            <th></th>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="修改" Width="50px" />
                            </td>
                        </tr> 
                        <tr>
                            <th>文件替换
                            </th>
                        </tr>
                        <tr>
                            <th></th>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" /><asp:HiddenField ID="hf_file" runat="server" /> 
                                <asp:Button ID="Button_upload" runat="server" Height="21px" OnClick="Button_upload_Click" Text=" 上 传 " Width="70px" /> 
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="Label1" runat="server" CssClass="file"></asp:Label></th>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="添加" CssClass="btn btn-warning btnyd" /></div> 
                </ContentTemplate>
            </asp:UpdatePanel> 
        </div> 
         <asp:HiddenField ID="HF_CMD" runat="server" />
         <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
