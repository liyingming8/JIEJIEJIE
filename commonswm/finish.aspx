﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="finish.aspx.cs" Inherits="commonswm_finish" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width" />
    <link href="../include/MasterPage.css" rel="stylesheet" />
    <link rel="stylesheet" href="../layuiadmin/layui/css/layui.css" media="all" />
    <link href="../include/public_v1.css" rel="stylesheet" />
    <title>三维码激活</title>
    <style type="text/css">
        body { font-size: 1.3rem; line-height: 2rem; } 
        .bg { position: absolute; top: 0; left: 0; right: 0; bottom: 0; z-index: -1; background-image: url('img/swmback.jpg'); background-repeat: no-repeat; font-size: 1.5rem; -ms-background-size: 100% 100%; background-size: 100% 100%; } 
        .topline { padding: 1rem; text-align: center; font-size: 1.5rem; font-weight: bold; margin-bottom: 1rem; } 
        .line { padding: 0.5rem 1rem; } 
            .line span { font-weight: bold; } 
        .bottomline { margin-top: 2rem; text-align: center; font-weight: bold; } 
        .finalline { margin-top: 10rem; text-align: center; } 
        .loginline { margin-bottom: 2rem; text-align: center; } 
        select { font-size: 1.1rem; line-height: 1.5rem; height: 2rem; margin-left: 0.3rem; border: 0.1rem solid #ff0000; -ms-border-radius: 0.2rem; border-radius: 0.2rem; } 
        .btnactive { background-color: rgb(235,97,0); color: #fff !important; padding: 0.2rem 0; height: 2.8rem;width: 75% !important; -ms-border-radius: 0.2rem !important; border-radius: 0.2rem !important; }
        .btngo { background-color: rgb(0,189,3); color: #fff !important; padding: 0.2rem 0; height: 2.8rem;width: 75% !important;  -ms-border-radius: 0.2rem !important; border-radius: 0.2rem !important; }
        .btnclear { background-color: rgb(150,150,150); color: #fff !important; padding: 0.2rem 0; height: 2.8rem;width: 75% !important;  -ms-border-radius: 0.2rem !important; border-radius: 0.2rem !important; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bg"></div>
        <div style="margin-top: 1.5rem; padding: 0.5rem;">  
            <div class="line finalline" id="divout"><input type="button" id="btn_" class="btn btn-info btnyd" style="width: 60%;" value="退  出"/></div> 
            <input id="isactive" type="hidden" value="0" runat="server" /> 
            <asp:HiddenField runat="server" ID="hf_compid" />
        </div>
    </form>
    <script src="../include/js/jquery-2.1.1.min.js"></script>
    <script src="../include/js/public_v1.js"></script>
    <script type="text/javascript" src="https://res.wx.qq.com/open/js/jweixin-1.2.0.js"></script>
    <script type="text/javascript"> 
        document.getElementById("btn_").onclick = close;
        function close() {
            WeixinJSBridge.call('closeWindow');
        } 
    </script>
</body>
</html>
