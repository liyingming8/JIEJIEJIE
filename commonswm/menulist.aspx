<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menulist.aspx.cs" Inherits="commonswm_menulist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="renderer" content="webkit" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0,user-scalable=0,uc-fitscreen=yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title>栅格化</title>
    <meta name="keywords" content="miniMobile的demo" />
    <meta name="description" content="miniMobile是一个简单易用的移动框架！" />
    <!-- miniMObile.css、js -->
    <link rel="stylesheet" type="text/css" href="miniMobile/css/miniMobile.css" />
    <script type="text/javascript" src="miniMobile/js/zepto.min.js"></script>
    <script type="text/javascript" src="miniMobile/js/miniMobile.js"></script>
    <!-- fonticon -->
    <link rel="stylesheet" type="text/css" href="miniMobile/plugins/fonticon/iconfont.css" />
    <!-- animate.css -->
    <link href="miniMobile/css/animate.css" rel="stylesheet" />
</head>
<body class="pb1 fadeIn animated color4">
    <form id="form1" runat="server">
        <header class="ui-header clearfix w75 h8 f46 pl3 pr3 color8 bg-color-primary t-c">
            <div class="ui-header-l fl w5">
                <a href="home.aspx" class="icon color8 iconfont icon-home_light"></a>
            </div>
            <div class="ui-header-c fl f30 w59">
                栅格化
            </div>
            <div class="ui-header-r fr w5">
                <%-- <i class="icon iconfont icon-phone"></i>--%>
            </div>
        </header>
        <section class="demo-nav t-c f28 clearfix">
            <%= QuickLingString %>
            <style>
                .demo-nav {
                    line-height: 1.8em;
                }
			
                .demo-nav div {
                    border-left: 1px solid #f1f1f1;
                    border-bottom: 1px solid #f1f1f1;
                }
			
                .demo-nav a {
                    display: block;
                    width: 100%;
                    height: 100%;
                }
			
                .demo-nav div:nth-child(4n+1) {
                    border-left: none;
                }
            </style>
        </section>
    </form>
</body>
</html>
