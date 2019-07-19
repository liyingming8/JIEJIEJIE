<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showactive.aspx.cs" Inherits="yanshi_showactive" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />


    <style>
        body {
            position: relative;
            min-height: 100%;
            margin: 0;
            padding: 0;
        }

        li {
            margin-left: 20%;
            list-style: none;
        }

        .imgn {
            width: 80%;
        }

        .divwhole {
            text-align: center;
            width: 80%;
        }

        .div1 {
            display: block;
        }

        .div2 {
            display: none;
        }

        .div3 {
            display: none;
        }

        .bottom {
            margin-left: 45%;
            position: fixed;
            bottom: 10px;
            height: 45px;
        }

        .btn {
            width: 150px;
            font-size: 18px;
            height: 40px;
            color: white;
            border-radius: 9px;
            border: none;
            background-color: forestgreen;
        }
    </style>


    <%--        <script src="../js/jquery-2.1.1.min.js"></script>
        <script src="../js/layer/layer.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div class="divwhole">
            <%--  <div class="div1" id="div1">--%>
            <div id="videoshow2">
                <video style=" width:1400px;height:760px;margin-top:-15px;margin-left:70px" autoplay loop controls="controls">
                    <source src="video/20190308_023256.mp4" type="video/mp4" /></video>
            </div>
            <%-- <ul>
                    <li>
                        <h2>第一步：设置活动名称(春节)</h2>
                        <img class=" imgn" src="img/huodong/ac3.png" /></li>
                    <li>
                        <h2>第二步：制定活动策略</h2>
                        <img  class=" imgn" src="img/huodong/ac4.png" /></li>
                </ul>--%>
            <%--  </div>--%>
           <%-- <div class="bottom">
                <input id="btn1" class="btn" type="button" value="立即体验" />
            </div>--%>
            <div class="div2" id="div2">
                <ul>


                    <li>
                        <h2>第一步：设置活动名称(情人节)</h2>
                        <img class=" imgn" src="img/huodong/ac5.png" />
                    </li>
                    <li>
                        <h2>第二步：制定活动策略</h2>
                        <img class=" imgn" src="img/huodong/ac6.png" /></li>
                </ul>

                <div class="bottom">
                    <input id="btn2" class="btn" type="button" value="立即体验" />
                </div>
            </div>
            <div class="div3" id="div3">
                <ul>
                    <li>
                        <h2>第一步：设置活动名称(中秋节)</h2>
                        <img class=" imgn" src="img/huodong/ac1.png" /></li>
                    <li>
                        <h2>第二步：制定活动策略</h2>
                        <img class=" imgn" src="img/huodong/ac2.png" /></li>
                </ul>
                <div class="bottom">
                    <input id="btn3" class="btn" type="button" value="立即体验" />
                </div>

            </div>
        </div>
        <script src=" ../js/jquery-2.1.1.min.js"></script>
        <script src="../js/jquery.easyui.min.js"></script>
        <script src="../js/layer/layer.js"></script>
        <script>


            $(document).ready(function () {
                var mmnum =<%=num%>;

                if (mmnum == "1") {
                    $("#btn1").click(function () {
                        layer.open({
                            title: '体验码',
                            type: 1,
                            skin: 'layui-layer-rim', //加上边框
                            area: ['35%', '60%'], //宽高
                            content: '<div style="text-align:center" >  <img src="img/ewmys.png" /></div>'
                        })

                    });

                }
                if (mmnum == "2") {
                    $("#btn2").click(function () {
                        layer.open({
                            title: '体验码',
                            type: 1,
                            skin: 'layui-layer-rim', //加上边框
                            area: ['35%', '60%'], //宽高
                            content: '<div style="text-align:center" >  <img src="img/huodong/qrj.png" style="with:90%" /><h2>验证码：844807<h2></div>'
                        })

                    });
                    $("#div1").css("display", "none");
                    $("#div2").css("display", "block");

                }
                if (mmnum == "3") {
                    $("#btn3").click(function () {
                        layer.open({
                            title: '体验码',
                            type: 1,
                            skin: 'layui-layer-rim', //加上边框
                            area: ['35%', '60%'], //宽高
                            content: '<div style="text-align:center" >  <img src="img/huodong/zqj.png" /><h2>验证码：531299<h2></div>'
                        })

                    });
                    $("#div1").css("display", "none");
                    $("#div2").css("display", "none");
                    $("#div3").css("display", "block");
                }


            })

        </script>
    </form>
</body>
</html>
