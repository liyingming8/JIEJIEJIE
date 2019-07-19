<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hdsz.aspx.cs" Inherits="yanshi_hdsz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            margin: 0;
            padding: 0;
            background-image: url(http://localhost:52019/yanshi/img/tjfw.jpg);
            background-repeat: no-repeat;
            background-size: cover;
        }

        img {
            margin-top: 10px;
            max-width: 95%;
        }

        p {
            font-size: 20px;
            font-weight: bold;
        }

        input {
            width: 200px;
            height: 50px;
            background-color: forestgreen;
            border-radius: 8px;
            border: 0px;
            color: white;
            font-weight: bold;
            font-size: 20px;
        }

        li {
            list-style: none;
            line-height: 80px;
        }
    </style>
    <script>
        var nuum;
        function dianji(num) {
            switch (num) {
                case '1': window.location.href = "showactive.aspx?cpid=1"; break;
                case '2': window.location.href = "showactive.aspx?cpid=2"; break;
                case '3': window.location.href = "showactive.aspx?cpid=3"; break;
                default: window.location.href = "showactive.aspx?cpid=1"; break;

            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top: 10%">
            <div style="text-align: center;">
                <h1>多元化的活动模块选择！</h1>
            </div>
            <div style="text-align: center;">
                <ul style="margin-left: -5%;">
                    <li>
                        <input id="chunjie" type="button" value="春节活动模块" onclick="dianji('1')" /></li>
                    <li>
                        <input id="qinren" type="button" value="情人节活动模块" onclick="dianji('2')" /></li>
                    <li>
                        <input id="zhonngqiu" type="button" value="中秋节活动模块" onclick="dianji('3')" /></li>

                    <li style="line-height:10px;font-weight:bold">
                       <span >·</span><br />
                        <span>·</span><br />
                        <span >·</span><br />
                        <span>·</span><br />
                        <span>·</span>
                    </li>
                     
                     

                </ul>
            </div>
        </div>

    </form>
</body>
</html>
