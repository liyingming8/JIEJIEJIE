<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="commonswm_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="UTF-8" />
    <meta name="renderer" content="webkit" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0,user-scalable=0,uc-fitscreen=yes" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title>天鉴三维码</title> 
    <!-- miniMObile.css、js -->
    <link rel="stylesheet" type="text/css" href="miniMobile/css/miniMobile.css"/>
    <script type="text/javascript" src="miniMobile/js/zepto.min.js"></script>
    <script type="text/javascript" src="miniMobile/js/miniMobile.js"></script>
    <!-- fonticon -->
    <link rel="stylesheet" type="text/css" href="miniMobile/plugins/fonticon/iconfont.css" />
    <!-- swiper -->
    <link href="miniMobile/css/swiper.min.css" rel="stylesheet" /> 
    <script src="miniMobile/js/swiper.min.js"></script>
    <!-- animate.css -->
    <link href="miniMobile/css/animate.css" rel="stylesheet" />
</head>
<body class="fadeIn animated">
    <form id="form1" runat="server">
        <header class="ui-header clearfix w75 h8 f46 pl3 pr3 color8 bg-color-success t-c o-h">
			<div class="ui-header-l fl w5">
				<b class="icon iconfont icon-sortlight"></b>
			</div>
			<div class="ui-header-c fl f30 w59">
				<%=CompanyName %>
			</div>
			<div class="ui-header-r fl w5">
				<%--<i class="icon iconfont icon-phone"></i>--%>
			</div>
		</header>
		<!-- aside -->
		<aside class="ui-aside w40 bg-color-success f30">
			<div class="user p3 color8 clearfix">
				<div class="fl w10">
					<img src="<%=HeaderImageURL %>" class="w10 h10 radius-o" />
				</div>
				<div class="fr w22">
					<span>尹春芳</span><br />
					<font class="tag f28">12</font>
				</div>
			</div>
			<ul class="f30 mt2">
                <%= LeftMenuString %> 
			</ul>
		</aside>
		<style>
			/*只针对侧栏内容部分做简单的样式*/
			
			.ui-aside {
				line-height: 1.5em;
			}
			
			.ui-aside ul {
				border-top: 0.02rem solid #017da7;
			}
			
			.ui-aside li {
				line-height: 0.8rem;
				border-bottom: 0.02rem solid #017da7;
			}
			
			.ui-aside a {
				display: block;
			}
		</style>
		<script type="text/javascript">
			var aside = $(".ui-aside").asideUi({
				hasmask: true,
				size: "4rem",
				position: "left",
				sidertime: 300
			});
			$(".ui-header-l").on('touchend', function() {
				aside.toggle();
			})
		</script>
		<!-- swiper -->
		<section class="swiper-container h40">
			<div class="swiper-wrapper">
				<div class="swiper-slide"><img src='miniMobile/img/banner1.jpg' class="w75 h40" /></div>
				<div class="swiper-slide"><img src='miniMobile/img/banner2.jpg' class="w75 h40" /></div>
				<div class="swiper-slide"><img src='miniMobile/img/banner3.jpg' class="w75 h40" /></div>
			</div>
			<!-- Add Arrows -->
			<div class="swiper-button-next"></div>
			<div class="swiper-button-prev"></div>
			<!-- Add Pagination -->
			<div class="swiper-pagination"></div>
		</section>
		<script>
			var swiper = new Swiper('.swiper-container', {
				navigation: {
					nextEl: '.swiper-button-next',
					prevEl: '.swiper-button-prev',
				},
				pagination: {
					el: '.swiper-pagination',
				}
			});
		</script>
		<style>
			.swiper-button-next,
			.swiper-button-prev {
				/*swiper 默认图标适应性较差，使用rem单位规定左右按钮大小，图标大小*/
				width: 0.3rem !important;
				height: 0.5rem !important;
				background-size: cover !important;
				margin-top: -0.23rem !important;
			}
		</style>
		<!--  -->
		<div class="t-c f28 p6 color4 bg-color6">
			<h2 class="color-danger f46">
				结构三维码
			</h2>
			<p>为您打造最专业的平台</p>
		</div>
		<!-- 导航 -->
		<section class="demo-nav t-c f28 clearfix">
			<%= QuickLingString %>
		</section>
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
		<!-- 底部导航 -->
		<nav class="demo-bottomNav mt6 w75 h12 pt1 t-c f28 bg-color8 o-h clearfix">
			<div class="w15 fl">
				<a href="home.aspx">
					<i class="f46 icon iconfont icon-home_light"></i>
					<p>首页</p>
				</a>
			</div>
			<div class="w15 fl">
				<a href="miniMobile/list-button.html">
					<i class="f46 icon iconfont icon-comment"></i>
					<p>左滑</p>
				</a>
			</div>
			<div class="w15 fl">
				<a href="miniMobile/list.html">
					<i class="f46 icon iconfont icon-rank"></i>
					<p>列表</p>
				</a>
			</div>
			<div class="w15 fl">
				<a href="miniMobile/comment.html">
					<i class="f46 icon iconfont icon-list"></i>
					<p>评论</p>
				</a>
			</div>
			<div class="w15 fl">
				<a href="miniMobile/my.html">
					<i class="f46 icon iconfont icon-servicefill"></i>
					<p>我的</p>
				</a>
			</div>
		</nav>
		<style type="text/css">
			.demo-bottomNav {
				line-height: 1.8em;
				border-top:1px solid #F1F1F1;
			}
			
			.demo-bottomNav a {
				display: block;
				width: 100%;
				height: 100%;
			}
		</style>
    </form>
</body>
</html>
