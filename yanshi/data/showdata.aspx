﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showdata.aspx.cs" Inherits="yanshi_data_showdata" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta http-equiv="X-UA-Compatible" content="ie=edge"/>
    <link rel="stylesheet" href="css/index.css"/>
    <title>信息化平台大数据</title>
    <script src="js/jquery-2.2.1.min.js"></script>
    <script src="js/rem.js"></script>
    <script src="js/echarts.min.js"></script>
    <script src="js/guangxi.js"></script>
    <script src="js/index.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="t_container">
       <%-- <header class="t_header">
            <span>信息化平台大数据</span>
        </header>--%>
        <main class="t_main">
            <div class="t_left_box">
                <img class="t_l_line" src="img/left_line.png" alt="">
                <div class="t_mbox t_rbox">
                    <i></i>
                    <span>本月订单数</span>
                    <h2>18000</h2>
                </div>
                <div class="t_mbox t_gbox">
                    <i></i>
                    <span>本月新增会员</span>
                    <h2>1000</h2>
                </div>
                <div class="t_mbox t_ybox">
                    <i></i>
                    <span>一次消费会员</span>
                    <h2>600</h2>
                </div>
                <img class="t_r_line" src="img/right_line.png" alt="">
            </div>
            <div class="t_center_box">
                <div class="t_top_box">
                    <img class="t_l_line" src="img/left_line.png" alt="">
                    <ul class="t_nav">
                        <li>
                            <span>扫码人数</span>
                            <h1>5000000</h1>
                            <i></i>
                        </li>
                        <li>
                            <span>上月消费者增加数</span>
                            <h1>300000</h1>
                            <i></i>
                        </li>
                        <li>
                            <span>增值率</span>
                            <h1>60%</h1>
                        </li>
                    </ul>
                    <img class="t_r_line" src="img/right_line.png" alt="">
                </div>
                <div class="t_bottom_box">
                    <img class="t_l_line" src="img/left_line.png" alt="">
                    <div id="chart_3" class="echart" style="width: 100%; height: 3.6rem;"></div>
                    <img class="t_r_line" src="img/right_line.png" alt="">
                </div>
            </div>
             <div class="t_right_box">
                    <img class="t_l_line" src="img/left_line.png" alt="">
                    <div id="chart_4" class="echart" style="width: 50%; height: 4.6rem; position: absolute;"></div>
                    <header class="t_b_h">
                        <span>刷新次数</span>
                        <img src="img/end.png"></img>
                        <h3>35<span>次</span></h3>
                    </header>
                    <main class="t_b_m">
                        <img src="img/map.png" alt="">
                        <div class="t_b_box">
                            <span>服务器溫度</span>
                            <i></i>
                            <h2>23℃</h2>
                        </div>
                        <div class="t_b_box1">
                            <span>服务器内部湿度</span>
                            <i></i>
                            <h2>56RH</h2>
                        </div>
                        <div class="t_b_box2">
                            <span>网络信号</span>
                            <i></i>
                            <h2>-90dBm</h2>
                        </div>
                        <div class="t_b_box3">
                            <span>室内光线</span>
                            <i></i>
                            <h2>250LX</h2>
                        </div>
                    </main>
                    <img class="t_r_line" src="img/right_line.png" alt="">
            </div> 
            <div class="b_left_box">
                    <img class="t_l_line" src="img/left_line.png" alt="">
                    <div id="chart_2" class="echart" style="width: 100%; height: 3.6rem;"></div>
                    <img class="t_r_line" src="img/right_line.png" alt="">
            </div>
            <div class="b_center_box">
                    <img class="t_l_line" src="img/left_line.png" alt="">
                    <div id="chart_1" class="echart" style="width: 100%; height: 3.6rem;"></div>
                    <img class="t_r_line" src="img/right_line.png" alt="">
            </div>
            <div class="b_right_box">
                    <img class="t_l_line" src="img/left_line.png" alt="">
                    <h1 class="t_title">消费者中奖信息</h1>
                    <table class="t_table">
                        <thead>
                            <tr>
                                <th>中奖时间</th>
                                <th>中奖人</th>
                                <th>中奖电话</th>
                                <th>奖品</th>  
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>2018-02-06</td>
                                <td>张伟</td>
                                <td>13111345462</td>
                                <td>20积分</td>
                            </tr>
                            <tr>
                                <td>2018-02-02</td>
                                <td>王五</td>
                                <td>13589456879</td>
                                <td>电动剃须刀</td>
                            </tr>
                            <tr>
                                <td>2018-02-01</td>
                                <td>丽萍</td>
                                <td>13625468597</td>
                                <td>风扇</td>
                            </tr>
                            <tr>
                                <td>2018-01-06</td>
                                <td>张红</td>
                                <td>13859897845</td>
                                <td>30积分</td>
                            </tr>
                            <tr>
                                <td>2018-12-06</td>
                                <td>王涛</td>
                                <td>13945825684</td>
                                <td>20元话费</td>
                            </tr>
                        </tbody>
                    </table>
                    <img class="t_r_line" src="img/right_line.png" alt="">
            </div>
        </main>
    </div>
    </form>
</body>
</html>
