<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zwjb_ty.aspx.cs" Inherits="wx_zwjb_ty" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TB_SuYuanJianCeJiGou</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/windows.css" rel="stylesheet" />
    <style>
        body, html {
            overflow-y: hidden;
            overflow-x: hidden;
        }

        img {
            max-height: 100%; 
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <%--<p>请到体验区联系工作人员，拿实物标签进行“刷脸体验”</p>
                   
                   <p>感谢您的参与！</p>--%>
            <img src="img/swmys.png" style="max-width: 100%;" />

        </div>
        <asp:HiddenField ID="HF_CMD" runat="server" />
        <asp:HiddenField ID="HF_ID" runat="server" />
    </form>
    <script src="../include/js/jquery.min.js" type="text/javascript"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
</body>
</html>
