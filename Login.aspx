<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %> 
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>登入-天鉴综合信息化平台</title>
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link rel="stylesheet" href="layuiadmin/layui/css/layui.css" media="all" />
    <link rel="stylesheet" href="layuiadmin/style/admin.css" media="all" />
    <link rel="stylesheet" href="layuiadmin/style/login.css" media="all" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="layadmin-user-login layadmin-user-display-show" id="LAY-user-login" style="display: none;"> 
            <div class="layadmin-user-login-main">
                <div class="layadmin-user-login-box layadmin-user-login-header">
                    <h2>天鉴科技</h2>
                    <p>综合信息化平台</p>
                </div>
                <div class="layadmin-user-login-box layadmin-user-login-body layui-form">
                    <div class="layui-form-item">
                        <label class="layadmin-user-login-icon layui-icon layui-icon-username" for="LAY-user-login-username"></label>
                        <input type="text" runat="server" name="username" id="username" lay-verify="required" placeholder="用户名" class="layui-input" />
                    </div>
                    <div class="layui-form-item">
                        <label class="layadmin-user-login-icon layui-icon layui-icon-password" for="LAY-user-login-password"></label>
                        <input type="password" runat="server" name="password" id="userpassword" lay-verify="required" placeholder="密码" class="layui-input" />
                    </div>
                    <div class="layui-form-item">
                        <div class="layui-row">
                            <div class="layui-col-xs7">
                                <label class="layadmin-user-login-icon layui-icon layui-icon-vercode" for="LAY-user-login-vercode"></label>
                                <input type="text" name="vercode" runat="server" id="loginvercode" lay-verify="required" placeholder="图形验证码" class="layui-input" />
                            </div>
                            <div class="layui-col-xs5">
                                <div style="margin-left: 10px;">
                                  <img id="imgyanzhengma" src="yzm/yanzhengma.aspx" alt="点击换一个" title="点击换一个" style="cursor: auto; height: 2.29rem;" onclick="changeImg();" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="layui-form-item" style="margin-bottom: 20px;">
                        <input type="checkbox" name="remember" lay-skin="primary" title="记住密码" /> 
                    </div>
                    <div class="layui-form-item">
                        <input type="submit" runat="server" id="btn_ok" OnServerClick="Button_Login_Click" value="登入"  style="font-size: 1.3rem;" class="layui-btn layui-btn-fluid"/>  
                    </div>
                </div>
            </div>

            <div class="layui-trans layadmin-user-login-footer"> 
                <p>© 2018 <a href="http://www.china315net.com/" target="_blank">www.china315net.com</a></p>
            </div> 
        </div>
        <input  type="hidden" runat="server" id="iszc" value="0"  />
        <input  type="hidden" runat="server" id="isshow" value="0"  />
    </form>
    <script src="layuiadmin/layui/layui.js"></script>
    <script type="text/javascript"> 
        function shuruyzm() {
            layer.alert('请输入验证码!');
        }
        window.onload = function () { 
            if (document.getElementById("isshow").value == 1) { 
                document.getElementById("username").value="天鉴科技";
                document.getElementById("userpassword").value = "123456";
                document.getElementById("loginvercode").value = getCookie("tjyzm");
            }
        }

        function getCookie(name) {
            var strcookie = document.cookie;//获取cookie字符串
            var arrcookie = strcookie.split("; ");//分割
            //遍历匹配
            for (var i = 0; i < arrcookie.length; i++) {
                var arr = arrcookie[i].split("=");
                if (arr[0] == name) {
                    return arr[1];
                }
            }
            return "";
        }
    </script>
</body>
</html>
