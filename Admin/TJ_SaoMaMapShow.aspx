<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TJ_SaoMaMapShow.aspx.cs" Inherits="Admin_TJ_SaoMaMapShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link href="../include/public.css" rel="stylesheet" />
    <script src="../include/js/publicNew.js"></script>
    <link rel="stylesheet" href="http://cache.amap.com/lbs/static/main1119.css" />
    <script type="text/javascript" src="http://webapi.amap.com/maps?v=1.4.6&key=9db868ad8d169110c5643903108bdfac&plugin=AMap.MarkerClusterer"></script>
    <script type="text/javascript" src="https://cache.amap.com/lbs/static/addToolbar.js"></script>
    <title>扫码地图</title>
    <style>
        .nav {
            position: fixed;
            top: 2%;
            left: 7%;
            z-index: 99;
            width: 90%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="nav" style="display: none;">
            <span>选择日期：</span>
            <input type="date" id="input_date" value="" />
        </div>
        <div id="container">
        </div>
        <input type="hidden" id="TComp" runat="server" value="" />
    </form>
    <script src="https://a.amap.com/jsapi_demos/static/china.js"></script>

    <script>
        loading.open();
        var map = new AMap.Map('container', {
            center: [116.397428, 39.90923],
            zoom: 4
        });
        map.setMapStyle('amap://styles/db1975b33b5350905d2b0c11b81d7174');
        var cluster, markers = [];
        var map = new AMap.Map("container", {
            resizeEnable: true,
            center: [105, 34],
            zoom: 4
        });
        //配置地图↑

        var oInputDate = document.getElementById("input_date");
        var cx_date = getNowFormatDate();
        var oCompName = document.getElementById("TComp").value;
        if (oCompName == "湖南浏阳河酒业发展有限公司")
            oCompName = "湖南浏阳河酒厂有限公司";
        var arr = {
            <%=Positonstring%>  
        }
        console.log(arr);
        jsonpBack(arr);
        function jsonpBack(result) {
            console.log(result.data);
            for (var i = 0; i < result.data.length; i += 1) {
                markers.push(new AMap.Marker({
                    position: result.data[i][0],
                    content: '<div style="background-color: hsla(180, 100%, 50%, 0.7); height: 24px; width: 24px; border: 1px solid hsl(180, 100%, 40%); border-radius: 12px; box-shadow: hsl(180, 100%, 50%) 0px 0px 1px;"></div>',
                    offset: new AMap.Pixel(-15, -15)
                }))
            }
            var count = markers.length;
            var _renderCluserMarker = function (context) {
                var factor = Math.pow(context.count / count, 1 / 18)
                var div = document.createElement('div');
                var Hue = 180 - factor * 180;
                var bgColor = 'hsla(' + Hue + ',100%,50%,0.7)';
                var fontColor = 'hsla(' + Hue + ',100%,20%,1)';
                var borderColor = 'hsla(' + Hue + ',100%,40%,1)';
                var shadowColor = 'hsla(' + Hue + ',100%,50%,1)';
                div.style.backgroundColor = bgColor
                var size = Math.round(30 + Math.pow(context.count / count, 1 / 5) * 20);
                div.style.width = div.style.height = size + 'px';
                div.style.border = 'solid 1px ' + borderColor;
                div.style.borderRadius = size / 2 + 'px';
                div.style.boxShadow = '0 0 1px ' + shadowColor;
                div.innerHTML = context.count;
                div.style.lineHeight = size + 'px';
                div.style.color = fontColor;
                div.style.fontSize = '14px';
                div.style.textAlign = 'center';
                context.marker.setOffset(new AMap.Pixel(-size / 2, -size / 2));
                context.marker.setContent(div)
            }
            if (cluster) {
                cluster.setMap(null);
            }
            cluster = new AMap.MarkerClusterer(map, markers, {
                gridSize: 80,
                renderCluserMarker: _renderCluserMarker
            });

            loading.close();
        }
        function getNowFormatDate() {
            var date = new Date();
            var seperator1 = "-";
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = year + seperator1 + month + seperator1 + strDate;
            return currentdate;
        }
    </script>
</body>
</html>
