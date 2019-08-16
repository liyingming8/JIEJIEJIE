<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="views_index" %> 
<!DOCTYPE html> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
  <title>天鉴综合信息化平台</title>
  <meta name="renderer" content="webkit" />
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
  <link rel="stylesheet" href="../layuiadmin/layui/css/layui.css" media="all" />
  <link rel="stylesheet" href="../layuiadmin/style/admin.css" media="all" /> 
    <%--<style>
        /deep/ .layadmin-iframe .rows,.layadmin-iframe .top col-md-12{ display:none
        }

    </style>--%>
</head>
<body class="layui-layout-body">
    <form id="form1" runat="server">
   <div id="LAY_app">
    <div class="layui-layout layui-layout-admin">
      <div class="layui-header">
        <!-- 头部区域 -->
        <ul class="layui-nav layui-layout-left">
          <li class="layui-nav-item layadmin-flexible" lay-unselect>
            <a href="javascript:;" layadmin-event="flexible" title="侧边伸缩">
              <i class="layui-icon layui-icon-shrink-right" id="LAY_app_flexible"></i>
            </a>
          </li>
          <li class="layui-nav-item layui-hide-xs" lay-unselect>
            <a href="http://www.china315net.com/" target="_blank" title="天鉴科技">
              <i class="layui-icon layui-icon-website"></i>
            </a>
          </li>
          <li class="layui-nav-item" lay-unselect>
            <a href="javascript:;" layadmin-event="refresh" title="刷新">
              <i class="layui-icon layui-icon-refresh-3"></i>
            </a>
          </li>
          <li class="layui-nav-item layui-hide-xs" lay-unselect>
            <input type="text" placeholder="搜索..." autocomplete="off" class="layui-input layui-input-search" layadmin-event="serach" lay-action="template/search.html?keywords="> 
          </li>
        </ul>
        <ul class="layui-nav layui-layout-right" lay-filter="layadmin-layout-right">
          
          <li class="layui-nav-item" lay-unselect>
            <a lay-href="../Admin/TJ_PublishInfo.aspx" layadmin-event="message" lay-text="消息中心">
              <i class="layui-icon layui-icon-notice"></i>   
              <!-- 如果有新消息，则显示小圆点 -->
              <span class="layui-badge-dot"></span>
            </a>
          </li>
          <li class="layui-nav-item layui-hide-xs" lay-unselect>
            <a href="javascript:;" layadmin-event="theme">
              <i class="layui-icon layui-icon-theme"></i>
            </a>
          </li>
          <li class="layui-nav-item layui-hide-xs" lay-unselect>
            <a href="javascript:;" layadmin-event="note">
              <i class="layui-icon layui-icon-note"></i>
            </a>
          </li>
          <li class="layui-nav-item layui-hide-xs" lay-unselect>
            <a href="javascript:;" layadmin-event="fullscreen">
              <i class="layui-icon layui-icon-screen-full"></i>
            </a>
          </li>
          <li class="layui-nav-item" lay-unselect>
            <a href="javascript:;">
              <cite><%=UserName %></cite>
            </a>
            <dl class="layui-nav-child">
            <%--  <dd><a lay-href="set/user/info.html">基本资料</a></dd>--%>
              <dd><a lay-href="../Admin/TJ_ModifySinglePassword.aspx">修改密码</a></dd>
              <hr>
              <dd style="text-align: center;"><a href="..\Exit.aspx">退出</a></dd>
            </dl>
          </li>
          
          <%--<li class="layui-nav-item layui-hide-xs" lay-unselect>
            <a href="javascript:;" layadmin-event="about"><i class="layui-icon layui-icon-more-vertical"></i></a>
          </li>
          <li class="layui-nav-item layui-show-xs-inline-block layui-hide-sm" lay-unselect>
            <a href="javascript:;" layadmin-event="more"><i class="layui-icon layui-icon-more-vertical"></i></a>
          </li>--%>
        </ul>
      </div> 
      <!-- 侧边菜单 -->
      <div class="layui-side layui-side-menu">
        <div class="layui-side-scroll">
            <div class="layui-logo" lay-href="Admin/Welcome.aspx">
                <span><asp:Literal runat="server" ID="literal_compname"></asp:Literal></span>
            </div>
          
          <ul class="layui-nav layui-nav-tree" lay-shrink="all" id="LAY-system-side-menu" lay-filter="layadmin-system-side-menu">
            <li data-name="home" class="layui-nav-item layui-nav-itemed" id="left_menu" runat="server">
              <%--
              <a href="javascript:;" lay-tips="主页" lay-direction="2">
                <i class="layui-icon layui-icon-home"></i>
                <cite>主页</cite>
              </a>
              <dl class="layui-nav-child">
                <dd data-name="console" class="layui-this">
                  <a lay-href="../views/home/console.aspx">控制台</a>
                </dd> 
              </dl>
                  --%>
            </li>
            <asp:PlaceHolder runat="server" ID="ph_left_menu"></asp:PlaceHolder> 
            <li data-name="template" class="layui-nav-item">
              <a href="javascript:;" lay-tips="页面" lay-direction="2">
                <i class="layui-icon layui-icon-template"></i>
                <cite>页面</cite>
              </a>
              <dl class="layui-nav-child">  
                <dd><a onclick="http://www.baidu.com">百度一下</a></dd>
                <dd><a lay-href="http://www.china315net.com/">天鉴科技</a></dd> 
              </dl>
            </li>   
          </ul>
        </div>
      </div>

      <!-- 页面标签 -->
      <div class="layadmin-pagetabs" id="LAY_app_tabs">
        <div class="layui-icon layadmin-tabs-control layui-icon-prev" layadmin-event="leftPage"></div>
        <div class="layui-icon layadmin-tabs-control layui-icon-next" layadmin-event="rightPage"></div>
        <div class="layui-icon layadmin-tabs-control layui-icon-down">
          <ul class="layui-nav layadmin-tabs-select" lay-filter="layadmin-pagetabs-nav">
            <li class="layui-nav-item" lay-unselect>
              <a href="javascript:;"></a>
              <dl class="layui-nav-child layui-anim-fadein">
                <dd layadmin-event="closeThisTabs"><a href="javascript:;">关闭当前标签页</a></dd>
                <dd layadmin-event="closeOtherTabs"><a href="javascript:;">关闭其它标签页</a></dd>
                <dd layadmin-event="closeAllTabs"><a href="javascript:;">关闭全部标签页</a></dd>
              </dl>
            </li>
          </ul>
        </div>
        <div class="layui-tab" lay-unauto lay-allowClose="true" lay-filter="layadmin-layout-tabs">
          <ul class="layui-tab-title" id="LAY_app_tabsheader">
            <li lay-id="./home/console.html" lay-attr="./home/console.html" class="layui-this"><i class="layui-icon layui-icon-home"></i></li>
          </ul>
        </div>
      </div>
      
      
      <!-- 主体内容 -->
      <div class="layui-body" id="LAY_app_body" runat="server">
        <%--
        <div class="layadmin-tabsbody-item layui-show">
          <iframe src="../views/home/console.aspx" frameborder="0" class="layadmin-iframe" name="hreflink" id="ifq"></iframe>         
        </div>
            --%>
      </div>
        <%--<div id="login_box" class="login-box"></div>--%>
       <div class="layui-body" id="LAY_app_bodys" style="display:none">
        <div class="layadmin-tabsbody-item layui-show">
          <iframe src="http://47.107.230.103:8668/company/tj_os_login/?uid=<%=pub()%>" frameborder="0" class="layadmin-iframes" id="ifw"></iframe>
        </div>
      </div>
       <div class="layui-body" id="LAY_app_bodyt" style="display:none">
        <div class="layadmin-tabsbody-item layui-show">
          <iframe src="http://47.107.230.103:8668/" frameborder="0" class="layadmin-iframe" name="ifes" id="ife"></iframe>
        </div>
      </div>
      
      <!-- 辅助元素，一般用于移动设备下遮罩 -->
      <div class="layadmin-body-shade" layadmin-event="shade"></div>
    </div>
  </div>

  <script src="../layuiadmin/layui/layui.js"></script> 
  <script src="../js/jquery-2.1.1.min.js"></script>
  <script type="text/javascript">
      var myiframe =document.getElementById("ife");
          myiframe.src = myiframe.src;   
  </script>
  <script type="text/javascript"> 
      function uut() {
          //var myiframe =document.getElementById("ife");
          //myiframe.src = myiframe.src;
      }
      function uunr(url) {//点击iframe
          var ifw = $("#ife")[0].contentWindow;
          Cross.call(ifw, "http://47.107.230.103:8668/", "test", { t: url });
          document.getElementById("LAY_app_bodyt").style.display = "inline-block"
          document.getElementById("LAY_app_body").style.display="none"
      };               

      layui.config({
          base: '../layuiadmin/' //静态资源所在路径
      }).extend({
          index: 'lib/index' //主入口模块
      }).use('index');

      $(function() {
          if (window.history && window.history.pushState) {
              $(window).on('popstate', function() {
                  window.history.pushState('forward', null, '#');
                  window.history.forward(1);
              });
          }
          window.history.pushState('forward', null, '#'); //在IE中必须得有这两行
          window.history.forward(1);         
      });
  </script>        
    <script type="text/javascript">
        var ims = "1";
        var ins = "1"
        $(document).ready(function () {
            
            $('#LAY_app li a').click(function () {
                if (ims==ins) {
                    var myiframe =document.getElementById("ife");
                    myiframe.src = myiframe.src;
                        ims = "2";
                 }
            });           
            $('#LAY_app dd a').click(function () {
                //uut();
                var h = $(this).attr('lay-hrefgg');
                if (typeof h == "undefined") {
                    document.getElementById("LAY_app_bodyt").style.display = "none"                    
                    document.getElementById("LAY_app_body").style.display = "inline-block"
                }else {
                    uunr($(this).attr('lay-hrefgg'));
                }                
            });
        jQuery.RefreshContent=function(url){
            $.ajax({
                type:'GET',
                processData: false,
                contentType: 'application/json; charset=utf-8',
                url:url,
                success:function(html){
                    $('#data-content').html(html);
                    return;
                },
                error: function(XMLHttpRequest, textStatus, errorThrown) {
                    alert('连接服务器失败!');
                    return;
                }
                });
        };

        jQuery.SubmitForm=function(form,url){
            $(form).ajaxSubmit({
                type: 'post',
                url: url,
                //data: {
                //    'title': title,
                //    'content': content
                //},
                success: function(html) {
                    $('#data-content').html(html);
                }
                //$(this).resetForm(); // 提交后重置表单
            });
            return false; // 阻止表单自动提交事件
        };

        $(document).ajaxStart(function(){
 	            $('#ajax_loading2')
                .css({
                    top:200,
                    left:($(window).width() - 200) * 0.5
                })
                .show();
        });
        $(document).ajaxStop(function(){
	        $('#ajax_loading2').hide();
        });

        $( document).on( "click", " .l-a", function(e){
            $.RefreshContent($(this).attr('m-url'));
        });
        $( document).on( "click", " .l-lnk", function(e){
            $.RefreshContent($(this).attr('lnk'));
        });


        $('.left a').click(function(){
            var me=$(this);
            $('.left a').removeClass('active');
            $(me).addClass('active');
            });

        jQuery.isEmpty=function (value){
            if(value == null || value == "" || value == "undefined" || value == undefined || value == "null"){
                return true;
            }
            else{
                value = value.replace(/\s/g,"");
                if(value == ""){
                    return true;
                }
                return false;
            }
        };
    });

</script>
 <script>
     (function(global){
          global.Cross = {
            signalHandler: {},
            on: function(signal, func){
              this.signalHandler[signal] = func;
            },
            call: function(win, domain, signal, data, callbackfunc){
              var notice = {"signal": signal, "data": data};
              if(!!callbackfunc){
                  notice["callback"] = "callback_" + new Date().getTime();
                  Cross.on(notice["callback"], callbackfunc);
              }
                    var noticeStr = JSON.stringify(notice);
              win.postMessage(noticeStr, domain);

            }
          };
          $(window).on("message", function (e) {
            var realEvent = e.originalEvent,
            data = realEvent.data,
            swin = realEvent.source,
            origin = realEvent.origin,
            protocol;

            try {
                protocol = JSON.parse(data);
                var result = global.Cross.signalHandler[protocol.signal].call(null, protocol.data);
                if(!!protocol["callback"]){

                  Cross.call(swin, origin, protocol["callback"], {result: result});

                }
                if(/^callback_/.test(protocol.signal)){

                  delete Cross.signalHandler[protocol.signal];

                }        
            } catch (e) {

            console.log(e);

            throw new Error("cross error.");
            }

           });

     })(window);
</script>


 </form>
</body>
</html>
