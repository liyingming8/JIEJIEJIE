<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_TraceInfoAddEdit.aspx.cs" Inherits="Admin_TJ_TraceInfoAddEdit" %> 
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_TraceInfo</title>
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
                    <td>所属产品</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ComboBox_WLProID" runat="server" AppendDataBoundItems="True" DataTextField="Products_Name" DataValueField="Infor_ID">
                            <asp:ListItem Value="0">所属产品...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>项目</th>
                    <td colspan="2">
                        <asp:DropDownList ID="ComboBox_CID" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>显示顺序</th>
                    <td>
                        <input id="inputShowOrder" runat="server" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" maxlength="4" class="input8" /></td>
                </tr>
                <tr>
                    <th>内容</th>
                    <td colspan="2">
                        <CKEditor:CKEditorControl ID="CKEditorControl_Contents" runat="server">
                        </CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr>
                    <th>图片</th>
                    <td colspan="2">
                        <asp:Image ID="Image_Pic" runat="server" /><iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no"
                            src="Attachment.aspx?PicUrl=upload&amp;TargetImg=Image_Pic&amp;TargetHd=HF_ImgURL&amp;imgMaxSize=200"
                            style="vertical-align: text-bottom" width="250"></iframe>
                    </td>
                </tr> 
            </table>
            <div class="bottomdivbutton"><asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" OnClick="Button1_Click" Text="添加" /></div>
        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
        <asp:HiddenField ID="HF_ImgURL" runat="server" />
    </form>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
