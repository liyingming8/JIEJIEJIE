<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showindex.aspx.cs" Inherits="wx_showindex" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <%--  <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />--%>
    <%--表示弹出啊窗口的样式--%>



    <style type="text/css">
        html {
            height: 100%;
        }

        body {
            position: relative;
            min-height: 100%;
            margin: 0;
            padding: 0;
        }

        .footershow {
            position: fixed;
            top: 25px;
            right: 5%;
            /*_position: absolute;*/
            /*_top: expression(document.documentElement.clientHeight + document.documentElement.scrollTop - this.offsetHeight);*/
        }

        .home-video {
            z-index: 100;
            position: absolute;
            top: 50%;
            left: 50%;
            min-width: 100%;
            min-height: 100%;
            object-fit: fill; /*这里是关键*/
            width: auto;
            height: auto;
            -ms-transform: translateX(-50%) translateY(-50%);
            -webkit-transform: translateX(-50%) translateY(-50%);
            transform: translateX(-50%) translateY(-50%);
            background: url(../video/cover.jpg) no-repeat;
            background-size: cover;
        }

        ::-webkit-scrollbar {
            width: 1px; 
            background-color: #F5F5F5;
        }

        /*定义滚动条轨道 内阴影+圆角*/
        ::-webkit-scrollbar-track {
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
            border-radius: 10px;
            background-color: #F5F5F5;
        }

        /*定义滑块 内阴影+圆角*/
        ::-webkit-scrollbar-thumb {
            border-radius: 10px;
            -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,.3);
            background-color: #555;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class=" div_WholePage" id="con">



            <div id="showdiv" style="overflow-y: auto">
            </div>





        </div>

        <div id="footershow" class="footershow">
            <input type="button" id="finput" value="立即体验" class="btn btn-warning btnyd" style="margin-bottom: 15px; height: 45px; width: 150px; font-size: 20px; border: none; color: white; border-radius: 9px; background-color: forestgreen" />

        </div>
        <asp:HiddenField ID="hf_show" runat="server" />
        <asp:HiddenField ID="hf_inputbuzou" runat="server" />
        <asp:HiddenField ID="hf_end" runat="server" />

        <script src=" ../js/jquery-2.1.1.min.js"></script>

        <script src="../js/layer/layer.js"></script>

        <script src="js/UploadImage.js"></script>
        <script>

            //$('.home-video').height(window.innerHeight);
            $('.header').height(window.innerHeight);
            $(window).resize(function () {
                $('.home-video').attr('height', window.innerHeight);
                $('.home-video').attr('width', window.innerWidth);
                $('.header').height(window.innerHeight);
            });

            function resizeVideo() {
                $('.home-video').attr('height', $(window).height());
                //console.log(window.innerHeight);
                $('.home-video').attr('width', window.innerWidth);
            }

            var gourl ="<%=url%>";
            var amod ="<%=moid%>";
            var imgurl;
            $(document).ready(function () {

                $('#showdiv').load(gourl);
            })
            if (amod == "1") {
                imgurl = '<div style="text-align:center" > <img src="img/ewmys.png" /></div>';
                //$("#videoshow").css("display", "block");
                //$("#videoshow").css("text-align", "center");
                $("#con").append('<div id="videoshow"> <video class="home-video" autoplay loop controls="controls" > <source src="img/chuanhuoyujing.mp4" type="video/mp4" /> </video></div>');
                resizeVideo();
                $('#showdiv').css("display", "none");

            } else if (amod == "3" || amod == "4") {
                imgurl = '<div style="text-align:center" > <img src="img/ewmys.png" /></div>';
                $("#footershow").css({ "top": "unset", "bottom": "25px" });
                $("#finput").css("background-color", "rgba(255,0,0,.6)");
            }
            else if (amod == "6") {
                imgurl = '<div style="text-align:center" >  <img src="img/dj3.png" /></div>';
            }
            else if (amod == "7" || amod == "5") { $("#footershow").css("display", "none"); }
            else {
                imgurl = '<div style="text-align:center" >  <img src="img/swmys.png" /></div>';
                //$("#videoshow2").css("display", "block");
                // $("#videoshow2").css("text-align", "center");
                $("#con").append('<div id="videoshow2" ><video class="home-video" autoplay loop controls="controls"><source src="img/jiahuoyujing.mp4" type="video/mp4" /></video></div>');
                resizeVideo();
                $('#showdiv').css("display", "none");
            }




            $(document).ready(function () {
                $("#finput").click(function () {
                    layer.open({
                        title: '体验码',
                        type: 1,
                        skin: 'layui-layer-rim', //加上边框
                        area: ['35%', '60%'], //宽高
                        content: imgurl
                    })

                });

            })
        </script>
    </form>
</body>
</html>
