<%@ Page Language="C#" AutoEventWireup="true" CodeFile="consoleswm.aspx.cs" Inherits="views_home_consoleswm" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我的控制台</title>
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <link rel="stylesheet" href="../../layuiadmin/layui/css/layui.css" media="all" />
    <link rel="stylesheet" href="../../layuiadmin/style/admin.css" media="all" />
    <style>
        .shink-red { animation: shink 3s infinite;}

        @-webkit-keyframes shink {
            from, 50%, to { color:red; }

            25%, 75% { color:#000 }
        }

        @keyframes shink {
            from, 50%, to { color:red; }

            25%, 75% { color:#000 }
        }

        .plink {
            font-size: 1.5rem !important;
            padding: 1.3rem 0.1rem;
            text-align: center;
        }
        .plink cite {
            font-size: 1.18rem !important;
            font-weight: bold !important;
        }
        
        .plink .key {
            color: #FF5722;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="layui-fluid">
            <div class="layui-row layui-col-space15">
                <div class="layui-col-md8">
                    <div class="layui-row layui-col-space15">
                        <div class="layui-col-md6">
                            <div class="layui-card">
                                <div class="layui-card-header">快捷方式</div>
                                <div class="layui-card-body"> 
                                    <div class="layui-carousel layadmin-carousel layadmin-shortcut">
                                        <div carousel-item>
                                            <ul class="layui-row layui-col-space10">
                                                <asp:Literal runat="server" ID="literal_kuaijiefangshi"></asp:Literal>
                                            </ul> 
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div id="div_swmconfig" runat="server" class="layui-col-md6">
                            <div class="layui-card">
                                <div class="layui-card-header">个性化配置</div>
                                <div class="layui-card-body"> 
                                    <div class="layui-carousel layadmin-carousel layadmin-backlog">
                                        <div carousel-item>
                                            <ul class="layui-row layui-col-space10">
                                                 <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_CompFrontPage_ConfigAddEdit.aspx" class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite class="key">基础设定</cite></p>
                                                    </a>
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_SWM_ActivedPidInfo.aspx"  class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite class="key">激活记录</cite></p>
                                                    </a>
                                                </li>  
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_RegisterCompanysQYJS.aspx"  class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite><asp:Label ID="LabelQyjs" runat="server" Text="">企业介绍</asp:Label></cite></p>
                                                    </a>
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_GoodsInfoSimple.aspx" class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite>上架商品</cite></p>
                                                    </a>
                                                </li>  
                                            </ul>
                                            <ul class="layui-row layui-col-space10">
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_Comp_ZhengShu.aspx"  class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite>资质证书</cite></p>
                                                    </a>
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_RegisterCompanysLXWM.aspx" class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite>联系我们</cite></p>
                                                    </a> 
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_CompADInfoSimple.aspx" class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite>广告图像</cite></p>
                                                    </a> 
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a lay-href="../Admin/TJ_PublishInfo.aspx" class="layadmin-backlog-body"> 
                                                        <p class="plink"><cite class="lay-text">新闻动态</cite></p>
                                                    </a> 
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="div_daiban" runat="server" class="layui-col-md6">
                            <div class="layui-card">
                                <div class="layui-card-header">待办事项</div>
                                <div class="layui-card-body">

                                    <div class="layui-carousel layadmin-carousel layadmin-backlog">
                                        <div carousel-item>
                                            <ul class="layui-row layui-col-space10">
                                                <li class="layui-col-xs6">
                                                    <a href="javascript:;" onclick="layer.tips('功能模块暂不可用', this, {tips: 3});" class="layadmin-backlog-body">
                                                        <h3>待审评论</h3>
                                                        <p><cite>66</cite></p>
                                                    </a>
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a href="javascript:;" onclick="layer.tips('功能模块暂不可用', this, {tips: 3});" class="layadmin-backlog-body">
                                                        <h3>待审帖子</h3>
                                                        <p><cite>12</cite></p>
                                                    </a>
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a href="javascript:;" onclick="layer.tips('功能模块暂不可用', this, {tips: 3});" class="layadmin-backlog-body">
                                                        <h3>待审商品</h3>
                                                        <p><cite>99</cite></p>
                                                    </a>
                                                </li>
                                                <li class="layui-col-xs6">
                                                    <a href="javascript:;" onclick="layer.tips('功能模块暂不可用', this, {tips: 3});" class="layadmin-backlog-body">
                                                        <h3>待发货</h3>
                                                        <p><cite>20</cite></p>
                                                    </a>
                                                </li>
                                            </ul>
                                            <ul class="layui-row layui-col-space10">
                                                <li class="layui-col-xs6">
                                                    <a href="javascript:;" onclick="layer.tips('功能模块暂不可用', this, {tips: 3});" class="layadmin-backlog-body">
                                                        <h3>待审友情链接</h3>
                                                        <p><cite style="color: #FF5722;">5</cite></p>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="layui-col-md12">
                            <div class="layui-card">
                                <div class="layui-card-header">数据概览</div>
                                <div class="layui-card-body">

                                    <div class="layui-carousel layadmin-carousel layadmin-dataview" data-anim="fade" lay-filter="LAY-index-dataview">
                                        <div carousel-item id="LAY-index-dataview">
                                            <div><i class="layui-icon layui-icon-loading layadmin-loading"></i></div>
                                            <div></div>
                                            <div></div>
                                        </div>
                                    </div>

                                </div>
                            </div> 
                        </div>
                    </div>
                </div>

                <div class="layui-col-md4">
                    <div class="layui-card">
                        <div class="layui-card-header">版本信息</div>
                        <div class="layui-card-body layui-text">
                            <table class="layui-table">
                                <colgroup>
                                    <col width="100" />
                                    <col />
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <td>当前版本</td>
                                        <td>
                                             V2.0.2 
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td>主要特色</td>
                                        <td>增加了多级经销商管理系统、大数据分析系统，营销活动系统功能和灵活性大幅提升，整个系统在性能上得到优化，兼容各种智能手机，与三维码激活平台、手持数据采集系统无缝连接</td>
                                    </tr>
                                    <tr>
                                        <td>获取渠道</td>
                                        <td style="padding-bottom: 0;">
                                            <div class="layui-btn-container">
                                                <a href="http://www.china315net.com" target="_blank" class="layui-btn layui-btn-danger">获取授权</a> 
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="layui-card">
                        <div class="layui-card-header">效果报告</div>
                        <div class="layui-card-body layadmin-takerates">
                            <div class="layui-progress" lay-showpercent="yes">
                                <h3>转化率（日同比 28% <span class="layui-edge layui-edge-top" lay-tips="增长" lay-offset="-15"></span>）</h3>
                                <div class="layui-progress-bar" lay-percent="65%"></div>
                            </div>
                            <div class="layui-progress" lay-showpercent="yes">
                                <h3>签到率（日同比 11% <span class="layui-edge layui-edge-bottom" lay-tips="下降" lay-offset="-15"></span>）</h3>
                                <div class="layui-progress-bar" lay-percent="32%"></div>
                            </div>
                        </div>
                    </div>

                    <div class="layui-card">
                        <div class="layui-card-header">实时监控</div>
                        <div class="layui-card-body layadmin-takerates">
                            <div class="layui-progress" lay-showpercent="yes">
                                <h3>CPU使用率</h3>
                                <div class="layui-progress-bar" lay-percent="58%"></div>
                            </div>
                            <div class="layui-progress" lay-showpercent="yes">
                                <h3>内存占用率</h3>
                                <div class="layui-progress-bar layui-bg-red" lay-percent="90%"></div>
                            </div>
                        </div>
                    </div> 
                </div> 
            </div>
        </div>
    </form>
    <script src="../../layuiadmin/layui/layui.js?t=1"></script>
    <script>
        layui.config({
            base: '../../layuiadmin/' //静态资源所在路径
        }).extend({
            index: 'lib/index' //主入口模块
        }).use(['index', 'console']);
    </script>
</body>
</html>
