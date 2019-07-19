<%@ Page Language="C#" AutoEventWireup="true" CodeFile="piccutter.aspx.cs" Inherits="Admin_getpic_piccutter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>图片裁剪</title>
    <link href="../../include/windows.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/cropper.css" />
    <link rel="stylesheet" href="css/cropperindex.css" />
    <style type="text/css">
        .content {
            display: flex;
            justify-content: center;
        } 
            .content img {
                max-height: 350px;
                max-width: 600px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bottomdivbutton" style="background-color: white; padding: 5px;">
            <input type="button" id="btn_save" runat="server" class="btn btn-warning btnyd" value="确定" onclick="returnback()" /><input type="button" style="font-weight: normal; font-size: 13px; background-color: lightblue; -ms-border-radius: 3px; border-radius: 3px; margin-left: 20px;" value="重新选择" class="btn btn-warning btnyd" onclick="    opencutter()" />
        </div>
        <div class="content">
            <asp:Image ID="imgforshow" scaleContent="true" maintainAspectRatio="false" runat="server" />
        </div> 
        <input type="hidden" runat="server" id="savefilepath" />
        <input type="hidden" runat="server" id="hdbilv" value="2" />
        <input type="hidden" runat="server" id="hdkey" /> 
        <input type="hidden" runat="server" id="hdsv"/>
    </form>
    <script type="text/javascript" src="js/jquery-2.1.0.js"></script>
    <script type="text/javascript" src="js/upImgauto.js"></script>
    <script type="text/javascript">
        function opencutter() {
            upImgauto($("#hdbilv").val(), 'imgforshow', 'savefilepath');
        }
        $(document).ready(opencutter());
        function returnback() {
            var key = $("#hdkey").val(); 
            var hdsv = $("#hdsv").val(); 
            this.parent.$("#" + key).attr("src", $("#savefilepath").val());
            this.parent.$("#" + hdsv).val($("#savefilepath").val());
            this.parent.$("#msgwindow").dialog("close");
        }
    </script> 
</body>
</html>
