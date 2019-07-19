<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SuYuanZhiQuGongYiAddEdit.aspx.cs" Inherits="Admin_TB_SuYuanZhiQuGongYiAddEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanZhiQuGongYi</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="True" EnableScriptLocalization="True" runat="server"></asp:ScriptManager>
        <div class="editpageback">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="gridtable">
                        <tr>
                            <th>工艺名称</th>
                            <td>
                                <input id="inputGongYiName" runat="server" type="text" maxlength="250"  /></td>
                        </tr>
                        <tr>
                            <th>工序</th>
                            <td>
                                <input id="inputShowerOrder" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4"  />
                            </td>
                        </tr>
                        <tr>
                            <th>图片</th>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Image ID="Image_Logo" Width="120px" runat="server" />
                                        </td>
                                        <td>
                                            <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                                                src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Logo&amp;TargetHd=HF_LogoImage&amp;imgMaxSize=200"
                                                style="vertical-align: text-bottom" width="250"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>描述</th>
                            <td>
                                <textarea id="inputDescription" cols="20" rows="3" runat="server"></textarea></td>
                        </tr>
                        <tr>
                            <th>备注</th>
                            <td>
                                <input id="inputRemarks" runat="server"  type="text" maxlength="250" /></td>
                        </tr>
                    </table>
                    <div class="bottomdivbutton">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btnyd" OnClick="Button1_Click" Text="添加" /></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
         <asp:HiddenField ID="HF_CMD" runat="server" />
         <asp:HiddenField ID="HF_ID" runat="server" />
         <asp:HiddenField runat="server" ID="HF_LogoImage" />
    </form>
     <script src="../include/js/jquery-1.7.1.js"></script>
     <script src="../include/js/My97DatePicker/WdatePicker.js"></script>
     <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
