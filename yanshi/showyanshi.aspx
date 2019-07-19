﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showyanshi.aspx.cs" Inherits="yanshi_showyanshi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>天鉴信息化平台</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=no" />




    <link href="../yanshi/css/animate.min.css?v=3" rel="stylesheet" />
    <script src="../yanshi/js/reset.js"></script>
    <link href="../yanshi/css/reset.css" rel="stylesheet" />
    <link href="../yanshi/css/swiper.css" rel="stylesheet" />
    <script src="../yanshi/js/swiper.js"></script>
    <script src="../yanshi/js/swiper.animate1.0.3.min.js"></script>
    <script src="../include/js/UploadImage.js" type="text/javascript"></script>
    <script src="../yanshi/js/public.js"></script>
    <link href="../js/layuiadmin/layui/css/layui.css" rel="stylesheet" />
    <style type="text/css">
        body, html {
            overflow-y: hidden;
            overflow-x: hidden; 
            background-color: #041E86;
            color:#FFF
        }

        .toast {  
            border-radius: 8px;
            position: fixed;
            margin: auto;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            z-index: 1;
        }

        .swiper-container {
            height: 100%;
        }

        @keyframes rotate {
            from {
                transform: rotateZ(0deg)
            }

            to {
                transform: rotateZ(360deg)
            }
        }

        .icon_music {
            position: fixed;
            top: 10px;
            right: 10px;
            z-index: 9998;
            width: 10%;
            animation: rotate 2s linear infinite
        }

        .logo {
            position: fixed;
            top: 35%;
            left: 20%;
            width: 60%;
            animation: rotate 3s linear infinite
        }

        .subtitle {
            font-size: 1.6rem;
            color: #EBBC7B;
            text-align: center;
            margin-top: 1.5rem;
        }

        .span1 {
            font-size: 1.3rem;
        }

        .subtitle1_img { 
            display:block;
            width: 50%;   
        }

        .icon_arrow {
            position: fixed;
            left: 42.5%;
            width: 15%;
            bottom: 5%;
            z-index: 9998;
            animation: fadeInUp 2s infinite;
        }

        .bg {
            position: absolute;
            top: 0;
            left: 0;
            z-index: -1;
            width: 100%;
        }

        .appcenter_share {
            position: relative;
            top: 1%;
            display: flex;
            /*flex-wrap: wrap;*/
            justify-content: space-between;
            padding: .5rem 3%;
        }

            .appcenter_share li {
                width: 5rem;
                height: 5rem;
                text-align: center;
                margin: .2rem 0;
            }

                .appcenter_share li a {
                    display: inline-flex;
                    align-items: center;
                    justify-content: center;
                    width: 3.2rem;
                    height: 3.2rem;
                    margin-top: .25rem;
                    border-radius: 2rem;
                }

                .appcenter_share li img {
                    height: 1.8rem;
                }

                .appcenter_share li p {
                    font-size: .55rem;
                    margin-top: .4rem; 
                    font-weight: bold;
                }

        .news-wrapper {
            padding: .5rem 5%;
            height: 60%;
            margin-top: 5%
        }

        .news-wrapper-title {
            font-size: .85rem;
            text-align: justify;
            color: #fff
        }

        .swm-img {
            display: flex;
            align-items: center;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-top: 5%;
            height: 80%;
            overflow: auto;
        }

            .swm-img a {
                width: 27%;
                margin: .3rem 0;
                padding: 2%;
                text-align: center;
                background-color: white;
                height: 3rem;
                overflow: hidden;
                display: flex;
                align-items: center
            }


        .loading-container {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 9999;
            background-color: rgba(0,0,0,.9);
        }

        .loading-wrapper {
            width: 60%;
            margin: 15rem auto 1rem;
            background-color: gray;
            height: 5px;
        }

        .loading-inner {
            width: 2%;
            height: 100%;
            background-color: white;
            transition: width .5s
        }

        .loading-info {
            text-align: center;
            font-size: .8rem;
            font-weight: bold;
            margin-top: .8rem;
            color: white
        }

        .video-item {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            margin: 5rem 0 0;
        }

            .video-item p {
                width: 100%;
                font-size: .8rem;
                margin: 1.5rem 0 .5rem;
                text-align: center;
                color: #fff
            }

        .more_video {
            font-size: .9rem;
            margin-top: 5rem;
            color: gray;
            text-align: center;
            text-decoration: underline
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="toast">
            <div class="swiper-container" id="main">
                <div class="swiper-wrapper">
                    <%--<div class="swiper-slide " >
                        <img class="bg ani"   />
                        <img src="img/subtitle1.png" class="subtitle1_img ani" swiper-animate-effect="fadeInDown" swiper-animate-duration="1.5s" swiper-animate-delay="1s" />
                    </div>--%>
                    <div class="swiper-slide " style="display:flex;align-items:center;align-content:center;justify-content:center;flex-wrap:wrap">
                        <img src="../yanshi/img/subtitle12.png" class="subtitle1_img ani" swiper-animate-effect="fadeInDown" swiper-animate-duration="1.5s" swiper-animate-delay="1s" />
                        <%--<img class="bg ani" swiper-animate-effect="fadeInDown" swiper-animate-duration="1s" />--%>


                        <ul class="appcenter_share" style="width:100%;margin-top:2rem">
                       <%--     <li onclick="go('../yanshi/swmjs.html',1)">
                                <a style="background-color: #f39800" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="1.8s">
                                    <img src="../yanshi/img/swmjs.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="3.5s">结构三维码</p>
                            </li>--%>
                            <li onclick="go('../yanshi/showindex.aspx?sw=7',1)">
                                <a style="background-color: #8fc31f" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="1.5s">
                                    <img src="../yanshi/img/qyjs.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="3.2s">节日活动模块</p>
                            </li>

                            <%-- <li onclick="go('zwjb.html',1)">
                                <a style="background-color: #448aca" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="2.1s">
                                    <img src="img/icon_zwjb.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="3.8s">真伪鉴别</p>
                            </li>--%>

                            <li onclick="go('../yanshi/showindex.aspx?sw=1',1)">
                                <a style="background-color: #556fb5" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="2.4s">
                                    <img src="../yanshi/img/icon_yujing.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="4.1s">窜货预警模块</p>
                            </li>
                            <li onclick=" go('../yanshi/showindex.aspx?sw=2',1)">
                                <a style="background-color: #f26521" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="3.6s">
                                    <img src="../yanshi/img/icon_shipin.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="5.3s">假货预警模块</p>
                            </li>
                            <li onclick="go('../yanshi/showindex.aspx?sw=3',1)">
                                <a style="background-color: #38b549" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="2.7s">
                                    <img src="../yanshi/img/icon_suoyuan.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="4.4s">溯源追溯模块</p>
                            </li>

                            <li onclick="go('../yanshi/showindex.aspx?sw=4',1)">
                                <a style="background-color: #f23645" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="3s">
                                    <img src="../yanshi/img/icon_caiji.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="4.7s">在线采集模块</p>
                            </li>
                            <li onclick=" go('../yanshi/showindex.aspx?sw=5',1)">
                                <a style="background-color: #ed135a" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="3.3s">
                                    <img src="../yanshi/img/icon_tongji.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="5s">流量统计模块</p>
                            </li>

                            <li onclick="go('../yanshi/showindex.aspx?sw=6',1)">
                                <a style="background-color: #cc0099" class="ani" swiper-animate-effect="rotateIn" swiper-animate-duration="2s" swiper-animate-delay="3.9s">
                                    <img src="../yanshi/img/icon_guanli.png" /></a>
                                <p class="ani" swiper-animate-effect="fadeIn" swiper-animate-duration="1s" swiper-animate-delay="5.6s">经销商管理系统模块</p>
                            </li>
                        </ul>
                    </div>
                    <div class="swiper-slide">
                        <img class="bg ani" src="img/bg3.jpg" swiper-animate-effect="fadeInDown" swiper-animate-duration="1s" />
                        <img src="../yanshi/img/shipinxinshang.png" style="width: 40%; margin-left: 30%" class="subtitle1_img ani" swiper-animate-effect="fadeInDown" swiper-animate-duration="1.5s" swiper-animate-delay="1s" />
                        <%--<div class="video-item ani" swiper-animate-effect="zoomInDown" swiper-animate-duration="1.5s" swiper-animate-delay="1.3s">
                    <iframe src='http://player.youku.com/embed/XMzYwMjk5MzA4MA==' frameborder=0 'allowfullscreen'></iframe>
                </div>--%>
                        <p onclick="go('demo/video.html',2)" class="more_video ani" swiper-animate-effect="zoomInUp" swiper-animate-duration="1.5s" swiper-animate-delay="2s">更多视频</p>
                    </div>
                    <div class="swiper-slide">
                        <img class="bg ani" src="img/bg3.jpg" swiper-animate-effect="fadeInDown" swiper-animate-duration="1s" />
                        <img src="img/huobaozixun.png" style="width: 40%; margin-left: 30%" class="subtitle1_img ani" swiper-animate-effect="fadeInDown" swiper-animate-duration="1.5s" swiper-animate-delay="1s" />
                        <div class="news-wrapper ">
                            <p class="news-wrapper-title ani" swiper-animate-effect="bounceIn" swiper-animate-duration="1.5s" swiper-animate-delay="1.5s">
                                <img src="img/timg.png" style="width: 5%;" /><span style="vertical-align: middle">“天鉴三维码”发布，商品进入“刷脸”时代！ 国内各大主流媒体争相报道 ！</span>
                            </p>
                            <div class="swm-img swiper-no-swiping">
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2s" onclick=" go('http://www.hainan.gov.cn/hn/yw/zwdt/tj/201806/t20180609_2654015.html',3)">
                                    <img src="img/logo/hngv_logo.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.1s" onclick="go('http://www.stdaily.com/cxzg80/guonei/2018-06/07/content_678662.shtml?from=singlemessage&isappinstalled=0',3)">
                                    <img src="img/logo/kjwsy_04.png" style="width: 90%" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.2s" onclick="go('http://www.hi.chinanews.com/hnnew/2018-06-09/464289.html',3)">
                                    <img src="img/logo/hainanxw.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.3s" onclick="go('http://new.qq.com/cmsn/20180611/20180611038322',3)">
                                    <img src="img/logo/qq_logo.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.4s" onclick="go('http://news.ifeng.com/a/20180609/58638985_0.shtml',3)">
                                    <img src="img/logo/fhw.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.5s" onclick="go('http://news.hainan.net/gundong/2018/06/09/3674665.shtml',3)">
                                    <img src="img/logo/hnzx_logo.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.6s" onclick="go('http://hnrb.hinews.cn/html/2018-06/09/content_2_4.htm',3)">
                                    <img src="img/logo/log.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.7s" onclick="go('http://www.jingji.com.cn/html/news/zxxw/107884.html',3)">
                                    <img src="img/logo/logo.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.8s" onclick="go('http://cn.dailyeconomic.com/roll/2018/06/11/13176.html',3)">
                                    <img src="img/logo/logo_mrjj.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="2.9s" onclick=" go('http://kc.china.com.cn/2018-06/11/content_40377817.htm',3)">
                                    <img src="img/logo/logo4.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="3s" onclick="go('http://nb.chinabyte.com/417/14528417.shtml',3)">
                                    <img src="img/logo/logoGIF1.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="3.1s" onclick="go('http://www.thethirdmedia.com/Article/201806/show407825c77p1.html',3)">
                                    <img src="img/logo/newlogo.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="3.2s" onclick="go('http://hainan.sina.com.cn/news/hnyw/2018-06-09/detail-ihcscwxa9460551.shtml',3)">
                                    <img src="img/logo/sina.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="3.3s" onclick="go('http://www.sohu.com/a/234693908_100122968',3)">
                                    <img src="img/logo/sohu.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="3.4s" onclick="go('https://weibo.com/5148524797/GkIByr1Ni?type=comment#_rnd1528726327304',3)">
                                    <img src="img/logo/sytt.png" /></a>
                                <a class="ani" swiper-animate-effect="bounceInLeft" swiper-animate-duration="1.5s" swiper-animate-delay="3.5s" onclick="go('http://m.hinews.cn/mpage.php?id=031463885&from=singlemessage&isappinstalled=0',3)">
                                    <img src="img/logo/waphi2.png" /></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="swiper-pagination"></div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        if (/Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent)) {
            $("#bg").attr('src', "img/bg3.jpg");
            $("#bg1").attr('src', "img/bg3.jpg");


        } else {
            $("#bg").attr('src', "img/swm.jpg");
            $("#bg1").attr('src', "img/swm.jpg");

        }
    </script>
    <!--</div>-->
    <script>
        //if (/Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent)) {
        //    window.location.href = "https://www.baidu.com/";
        //} else {
        //    window.location.href = "http://news.baidu.com/";
        //}
        var sound = document.getElementById("sound");
        sound.play();

        var index = public.getCookie("swiperSlide");
        var mySwiper = new Swiper('#main', {
            direction: 'vertical',
            watchSlidesProgress: true,
            pagination: '.swiper-pagination',
            updateOnImagesReady: true,
            preloadImages: true,
            on: {
                init: function () {
                    //打开新页面返回时，能跳到用户跳转前的位置；
                    if (index != "no" && index != 0) {
                        this.slideTo(index, 500, true);
                        public.setCookie("swiperSlide", 0);//始终重置
                    }
                    swiperAnimateCache(this); //隐藏动画元素
                    swiperAnimate(this); //初始化完成开始动画
                },
                imagesReady: function () {
                    document.getElementById("icon_arrow").style.display = "none";//第一页也要藏
                    //timer在上面的js中
                    clearInterval(timer);
                    oLoading_inner.style.width = "100%";
                    oLoading_info.innerText = "100%";
                    setTimeout(function () {
                        document.getElementById("loading-container").style.display = "none";
                    }, 700);
                    //自动跳转到第二页
                    if (index == 0 || index == "no") {
                        setTimeout(function () {
                            mySwiper.slideTo(1, 500, true);
                        }, 5000);
                    }
                },
                slideChangeTransitionEnd: function () {
                    if (this.activeIndex == 1) {
                        var oIcon_Module = document.querySelector(".appcenter_share").getElementsByTagName("a");
                        for (var i = 0; i < oIcon_Module.length; i++) {
                            oIcon_Module[i].removeAttribute("style");//针对再次进入不旋转
                        }
                        setTimeout(function () {
                            for (var i = 0; i < oIcon_Module.length; i++) {
                                oIcon_Module[i].style.animation = "rotate 6s linear infinite";//将两个模块旋转起来
                            }
                        }, 5000);
                    }
                    //判断是否要隐藏上划提示图片
                    if (this.activeIndex == 3)
                        document.getElementById("icon_arrow").style.display = "none";
                    else
                        document.getElementById("icon_arrow").style.display = "block";
                    swiperAnimate(this); //每个slide切换结束时也运行当前slide动画
                }, progress: function (progress) {

                }
            }
        });
        //mySwiper.slideTo(3, 500, true);
        //音乐图标暂停音乐播放音乐事件
        var flag = true,
            oIcon_Music = document.getElementById("icon_music");
        oIcon_Music.onclick = function () {
            if (flag) {
                oIcon_Music.style.animationPlayState = "paused"; sound.pause();
                flag = false;
            }
            else {
                oIcon_Music.style.animationPlayState = "running"; sound.play();
                flag = true;
            }
        }
        function go(url, slide) {
            public.setCookie("swiperSlide", slide);
            window.location.href = url;
        }

        function timest() {
            var tmp = Date.parse(new Date()).toString();
            tmp = tmp.substr(0, 10);
            return tmp;
        }
    </script>

    <script src="layuiadmin/layui/layui.js"></script>
    <script src="../yanshi/js/jquery-2.1.1.min.js"></script> 
</body>
</html>