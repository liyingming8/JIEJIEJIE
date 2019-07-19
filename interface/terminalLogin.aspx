<%@ Page Language="C#" AutoEventWireup="true" CodeFile="terminalLogin.aspx.cs" Inherits="_terminalLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <script src="../js/jquery-2.1.1.min.js"></script>
    <link rel="shortcut icon" href="#" />
    <title>终端店登录</title>
    <style>
        * {
            margin: 0;
            padding: 0;
        }
        input[type=button], input[type=submit], input[type=file], button { cursor: pointer; -webkit-appearance: none; }
        html, body {
            width: 100%;
            height: 100%;
        }

        body {
            background-image: linear-gradient(to bottom, #fff, #ccc);
        }


        .jiemian1 {
            background-color: RGB(250,255,255);
            -webkit-box-shadow: 1px 1px 1px 1px RGB(230,230,230);
            -ms-box-shadow: 1px 1px 1px 1px RGB(230,230,230);
            box-shadow: 1px 1px 1px 1px RGB(230,230,230);
            margin: 5em 0 0 0;
            padding: 1.5em 5% 3em 5%;
            display: inline-block;
            -ms-border-radius: 1em;
            border-radius: 1em;
            width: 80%;
        }


        .text {
            width: 90%;
            height: 2.5em;
            border: 1px solid RGB(200,200,200);
            line-height: 2.5rem;
            padding: 0 0.5rem;
            margin-bottom: 1rem;
            -ms-border-radius: 0.3rem;
            border-radius: 0.3rem;
            text-align: center;
            font-size: 1.2em;
        }

        .btn_lg {
            width: 90% !important;
            height: 2.5em;
            line-height: 2.5em;
            background-color: RGB(49,148,208);
            color: RGB(250,255,255);
            border: 0.1em solid RGB(49,148,208);
            -ms-border-radius: 0.3rem;
            border-radius: 0.3em;
            margin-top: 2rem;
            font-size: 1.3em;
            font-weight: bold;
        }

            .btn_lg:hover {
                background-color: RGB(24,124,183);
            }

        .denglu1 {
            margin-right: 1em;
            font-family: heiti;
            font-weight: 600;
            color: RGB(234,111,90);
            padding-bottom: 0.1em;
            font-size: 1.4em;
        }
    </style>


</head>
<body style="background-color: RGB(241,241,241);">
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <div class="jiemian1">
                <div style="margin: 2em 0  2em 0;">
                    <span class="denglu1" id="denglu1">积分商城登录</span>
                </div>
                <div id="jiemian1">
                    <input type="text" placeholder="终端店用户名" id="Lg_name" class="text" />
                    <input type="password" placeholder="密码" id="Lg_password" class="text" />
                    <input type="button" value="登    陆" onclick="login()" class="btn_lg" />
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function login() {
            var unm = $("#Lg_name").val();
            var pwd = $("#Lg_password").val();
            console.log(unm);
            console.log(pwd);
            if (unm.length > 0 && pwd.length > 0) {
                $.ajax({
                    url: "https://os.china315net.com/appajax/UserAuthorXiAnPost.ashx",
                    type: 'POST',
                    data: {
                        unm: unm,
                        upw: pwd
                    },
                    success: function (res) {
                        var obj = JSON.parse(res);
                        var rid = obj.rid;
                        if (rid === 160) {
                            window.location.href = 'https://tjfnew.china315net.com/integral_v2/terminalroute/xfterminal.ashx?terminalid=' + obj.coidnick + '&terminalnm=' + obj.unitnm;
                        } else {
                            alert("当前用户暂不能登录！");
                            $("#Lg_name").val('');
                            $("#Lg_password").val('');
                        }
                    }
                });
            } else {
                alert("请输入用户名和密码！");
                $("#Lg_name").val('');
                $("#Lg_password").val('');
            }
        }
    </script>
</body>
</html>
