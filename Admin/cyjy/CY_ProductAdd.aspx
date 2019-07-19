<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CY_ProductAdd.aspx.cs" Inherits="CY_ProductAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TJ_MSReport</title>
    <link href="../../include/windows.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="editpageback"> 
            <table class="gridtable">
                <asp:ScriptManager ID="ScriptManager1" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="server"></asp:ScriptManager>
                <tr>
                    <td>产品名称</td>
                    <td>
                         <input id="Pname" runat="server" type="text" maxlength="10" />
                       </td>
                       
                    <td></td>
                </tr>
                <tr>
                    <td>价格</td>
                    <td>
                        <input id="Price" runat="server" type="text" maxlength="10" />           </td>


                    <td></td>
                </tr>

                <tr>
                    <td>是否有效</td>
                    <td>
                        <asp:CheckBox ID="CheckBox_Use" runat="server" TextAlign="Left" />
                    </td>
                    <td></td>
                </tr>

                <tr>
                    <td>产品图片</td>
                    <td>
                        <asp:Image ID="Image_GoodPic" runat="server" Height="200px" />
                    </td>
                    <td>
                        <iframe id="I1" frameborder="0" height="23" name="I1" scrolling="no" src="Attachment.aspx?PicUrl=cpimg&amp;TargetImg=Image_GoodPic&amp;TargetHd=HF_ImageURL&amp;imgMaxSize=102400" style="vertical-align: text-bottom" width="250"></iframe>
                    </td>
                </tr>
                <tr>
                    <td>上传时间</td>
                    <td>
                        <asp:TextBox ID="inputTime" runat="server"></asp:TextBox>
                        <cc2:CalendarExtender TargetControlID="inputTime" ID="CalendarExtender1" Format="yyyy-MM-dd" runat="server">
                        </cc2:CalendarExtender>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>备注</td>
                    <td>
                        <input id="inputRemarks" runat="server" type="text" maxlength="25" /></td>
                    <td>
                        <asp:HiddenField ID="HF_FilePath" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="添加" /></td>
                    <td>
                        <asp:HiddenField ID="HF_CMD" runat="server" />
                        <asp:HiddenField ID="HF_ID" runat="server" />
                        <br />
                        <asp:HiddenField ID="HF_ImageURL" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </form>
    <script src="../../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
