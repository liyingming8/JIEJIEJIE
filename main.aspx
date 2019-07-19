<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="main" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title><asp:Literal ID="Literal_title" runat="server"></asp:Literal></title>
    <link href="include/default.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="js/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="js/themes/icon.css" /> 
    <script src="js/jquery-1.4.4.min.js"></script>
    <script src="js/jquery.easyui.min.js"></script>
    <script src="js/outlook2.js"></script>
    <script type="text/javascript">

        var _menus = {
            <%=MenuString %>
        };
        //设置登录窗口
        function openPwd() {
            $('#w').window({
                title: '修改密码',
                width: 300,
                modal: true,
                shadow: true,
                closed: true,
                height: 160,
                resizable: false
            });
        }
        //关闭登录窗口
        function closePwd() {
            $('#w').window('close');
        }
        //修改密码
        function serverLogin() {
            var $newpass = $('#txtNewPass');
            var $rePass = $('#txtRePass');

            if ($newpass.val() == '') {
                msgShow('系统提示', '请输入密码！', 'warning');
                return false;
            }
            if ($rePass.val() == '') {
                msgShow('系统提示', '请在一次输入密码！', 'warning');
                return false;
            }

            if ($newpass.val() != $rePass.val()) {
                msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
                return false;
            }

            $.post('Editpassword.ashx?uid=<%=UserId%>&newpass=' + $newpass.val(), function () {
                msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + $newpass.val(), 'info');
                $newpass.val('');
                $rePass.val('');
                this.close();
            });
            return false;
        }

        $(function () {

            openPwd();

            $('#editpass').click(function () {
                $('#w').window('open');
            });

            $('#btnEp').click(function () {
                serverLogin();
            });
            $('#btnCancel').click(function () { closePwd(); });
            $('#loginOut').click(function () {
                $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

                    if (r) {
                        location.href = 'Exit.aspx';
                    }
                });
            });
        });
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden"  scroll="yes">
    <noscript>
    <div style=" position:absolute; z-index:100000; height:2046px;top:0px;left:0px; width:100%; background:white; text-align:center;"> <img src="images/noscript.gif" alt='抱歉，请开启脚本支持！' /> </div>
    </noscript>
<%--<div region="north" split="true" style="overflow: hidden; height: 78px;background-color: #092B46; border:0;line-height: 22px;color: #fff; font-family: 微软雅黑,黑体,Verdana; background-attachment: inherit;"><table style="width: 100%;"><tr><td style="width: 10%;"><img alt="logo" style="margin-top: 6px;margin-left: 40px;height: 3rem;" src='<%=CompLogo %>'/></td><td style="color:white;font-size: 23px;font-weight: bold;">
    <asp:Literal ID="LiteralCompanyName" runat="server"></asp:Literal></td><td style="width: 33%;"><span style="color:#fff;font-size: 18px;font-weight: bold;" >您好: <%= UserName %>      <a href="#" id="editpass" style="color: #F48018;font-size: 18px;font-weight: bold;margin-left: 20px;">修改密码</a><br/>时间:<%=DateTime.Now.ToString("yyyy年MM月dd日") + "   "+ CommonFun.ChineseNameWeekDay(DateTime.Now.DayOfWeek.ToString()) %></span></td><td style="width: 80px;background-color: #D53F27;text-align: center;"><a href="#" id="loginOut"><img alt="out" src="images/out.png"/></a></td></tr></table></div>--%>
    <div region="north"  split="true" style="overflow: hidden; background-color: #092B46; border:0;line-height: 1rem;color: #fff; font-family: 微软雅黑,黑体,Verdana; background-attachment: inherit;"><div style="margin-top: 1.25rem;font-size: 1.5rem;float: left;margin-left: 1rem;font-weight: bold;"><asp:Literal ID="LiteralCompanyName" runat="server"></asp:Literal></div><div style="float: right;margin-right: 1rem;padding: 0;"><a href="#" id="loginOut"><img style="height: 4rem" alt="out" src="images/out.png"/></a></div></div>
<div region="south" split="true" style="height: 30px; background: #7FA2DC; ">
      <div class="footer" style="font-size: 16px;">
          <asp:Literal ID="Literal_BottomInfo" runat="server"></asp:Literal></div>
    </div>
<div region="west" hide="true" split="true" title="总体目录" style="width:166px;background-color: #2D3E50;" id="west">
      <div id="nav" class="easyui-accordion" fit="true" border="false"> 
    <!--  导航内容 -->  
  </div>
    </div>
<div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden">
      <div id="tabs" class="easyui-tabs"  fit="true" border="false" >
    <div title="欢迎使用" style="padding:0px;overflow:hidden;" > 
        <iframe style="border:none;width:100%;height:100%;padding:0px;margin:0px;" onscroll="true" src="<%= DefaultUrl %>"></iframe>
        </div>
  </div>
    </div> 

<!--修改密码窗口-->
<div id="w" class="easyui-window" title="修改密码" collapsible="false" minimizable="false"maximizable="false" icon="icon-save"  style="width: 300px; height: 150px; padding: 5px;background: #fafafa;">
      <div class="easyui-layout" fit="true">
    <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
          <table cellpadding=3>
        <tr>
              <td>新密码：</td>
              <td><input id="txtNewPass" type="Password" class="txt01" /></td>
            </tr>
        <tr>
              <td>确认密码：</td>
              <td><input id="txtRePass" runat="server" type="Password" class="txt01" /></td>
            </tr>
      </table>
        </div>
    <div region="south" border="false" style="text-align: right; height: 40px; line-height: 40px;"> <a id="btnEp" runat="server"  class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)" >确定</a> <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">取消</a> </div>
  </div>
    </div>
<div id="mm" class="easyui-menu" style="width:150px;">
      <div id="mm-tabupdate">刷新</div>
      <div class="menu-sep"></div>
      <div id="mm-tabclose">关闭</div>
      <div id="mm-tabcloseall">全部关闭</div>
      <div id="mm-tabcloseother">除此之外全部关闭</div>
      <div class="menu-sep"></div>
      <div id="mm-tabcloseright">当前页右侧全部关闭</div>
      <div id="mm-tabcloseleft">当前页左侧全部关闭</div>
      <div class="menu-sep"></div>
      <div id="mm-exit">退出</div>
    </div> 
</body>
</html>
