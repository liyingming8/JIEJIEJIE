<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FaHuoLYH.aspx.cs" Inherits="Admin_wuliu_Fahuo_FaHuoLYH" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.min.js"></script>
        <script type="text/javascript" src="../../uploadify/jquery.uploadify.min.js"></script>
        <link rel="stylesheet" type="text/css" href="../../uploadify/uploadify.css" />
 
        <title>上传多文件</title>
        <%--<script type="text/javascript">
        $(function () {
            $('#file_upload').uploadify({
                'auto': false,
                'swf': '../../uploadify/uploadify.swf',
                'uploader': '../../fahuo.aspx',
                'checkExisting': '../../uploadify/check-exists.php',
                'fileTypeDesc': '文本文件',
                'fileTypeExts': '*.txt',
                'onCancel': function (file) {
                    alert('文件 ' + file.name + ' 已删除')},
                'onUploadSuccess':function (file, data, response) {
                    $("#imgBox").html(data);
                },
                ' onUploadError': function (file, errorCode, errorMsg, errorString) {
                    alert('The file ' + file.name + ' could not be uploaded: ' + errorString);
                }
            
                // Your options here
            });
        });
    </script>--%>
    </head>
    <body>
        <form id="form1" runat="server">
            <%-- <p>
            <input type="file" name="file_upload" id="file_upload" />
        </p>
        <p><a href="javascript:$('#file_upload').uploadify('upload','*')">上传</a></p>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <p>
        <asp:Label ID="Label1" runat="server" Text="123123"></asp:Label>
        </p>--%>
        </form>
    </body>
</html>