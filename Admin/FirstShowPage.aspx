<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FirstShowPage.aspx.cs" Inherits="FirstShowPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="include/MasterPage.css" rel="stylesheet" />
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }

        .main {
            width: 100%;
            height: 100%;
            position: absolute;
        }

        .quarter-div {
            width: 49%;
            height: 49%;
            float: left;
        }

        .topleft {
            background-color: #fff;
            border-bottom: 1px solid #aaa;
        }

        .newscontent {
            padding: 8px 20px 30px 100px;
            text-align: center; 
        }
        .newscontent ul {
            margin-top: 20px;
            text-align: left;
        }
            .newscontent ul li {
                line-height: 30px;
                color: #333333;
                list-style: none;
            }

       a{color:#333333;text-decoration:none; }
       a:hover {color:#333333;text-decoration:none;}
        a:visited {color:#333333;text-decoration:none;}
        .topright {
            background-color: #fff;
            border-left: 1px solid #aaa;
            border-bottom: 1px solid #aaa;
        }

        .bottomleft {
            background-color: #F0AD4E;
             border-right: 1px solid #aaa; 
        }

        .bottomright {
            background-color: #FFC706;
        }

        .quarter-div .itemtitle {
            margin-top: 10px;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main"> 
            <div id="containerfahuoliang" class="quarter-div topleft"></div>
            <div class="quarter-div topright">
                <div class="newscontent">
                    <span style="font-size: 18px;color: #333333;width: 100%;text-align: left;">通知公告</span>
                    <ul>
                        <a href="#">
                            <li>【2017-08-10】团湘西州委携手酒鬼酒公司助力湘西优秀寒门学子</li>
                        </a>
                        <a href="#">
                            <li>【2017-08-04】美酒醉美食 “酒鬼酒”中国青年名厨精英赛</li>
                        </a>
                        <a href="#">
                            <li>【2017-08-03】招贤纳士——酒鬼酒股份有限公司招聘信息</li>
                        </a>
                        <a href="#">
                            <li>【2017-08-02】酒鬼酒推香港回归20周年纪念酒掀起收藏酒领域</li>
                        </a>
                        <a href="#">
                            <li>【2017-08-01】团湘西州委携手酒鬼酒公司助力湘西优秀寒门学</li>
                        </a>
                    </ul>
                </div>
            </div> 
            <div id="containerjingxiaoshang" class="quarter-div bottomleft"></div>
            <div id="containerfahuo" class="quarter-div bottomright"></div>
        </div>
    </form>
    <script type="text/javascript" src="js/tuxingshow/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="js/tuxingshow/highcharts.js"></script>
    <script type="text/javascript" src="js/tuxingshow/highcharts-3d.js"></script>
    <script type="text/javascript" src="js/tuxingshow/modules/exporting.js"></script>
    <script type="text/javascript" src="js/tuxingshow/highcharts-zh_CN.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#containerfahuo').highcharts({
                chart: {
                    type: 'pie',
                    options3d: {
                        enabled: true,
                        alpha: 45
                    }
                },
                title: {
                    text: '本月扫码量分布图'
                },
                subtitle: {
                    text: '2017年8月'
                },
                plotOptions: {
                    pie: {
                        innerSize: 100,
                        depth: 45
                    }
                },
                series: [{
                    name: '扫码量',
                    data: [
                        ['湖南', 8],
                        ['湖北', 3],
                        ['贵州', 1],
                        ['北京', 6],
                        ['河北', 8],
                        ['广东', 4],
                        ['云南', 4],
                        ['海南', 1],
                        ['四川', 1]
                    ]
                }],
                credits: {
                    enabled: false // 禁用版权信息
                }
            });

            $('#containerfahuoliang').highcharts({
                title: {
                    text: '厂级总体发货量'
                },
                xAxis: {
                    categories: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '19月', '11月', '12月']
                },
                yAxis: {
                    title: {
                        text: '发货量 (件)'
                    }
                },
                plotOptions: {
                    series: {
                        stacking: 'normal'
                    }
                },
                series: [{
                    type: 'column',
                    name: '内参',
                    data: [1200, 1000, 900, 1100, 850, 800, 650, 600, 700, 900, 850, 1300]
                },
                 {
                     type: 'column',
                     name: '洞藏',
                     data: [600, 700, 500, 400, 500, 660, 650, 400, 600, 800, 540, 890]
                 },
                 {
                     type: 'column',
                     name: '酒鬼酒',
                     data: [2200, 2300, 2100, 1800, 1900, 2000, 2150, 1880, 2100, 2500, 2450, 2300]
                 }, {
                     type: 'column',
                     name: '湘泉',
                     data: [3200, 4000, 3500, 3540, 3600, 3800, 2650, 2800, 2900, 2800, 3850, 3300]
                 }],
                credits: {
                    enabled: false // 禁用版权信息
                }
            }); 
            
            $('#containerjingxiaoshang').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: '一级经销商发货库存'
                },
                subtitle: {
                    text: '年度: 2017'
                },
                xAxis: {
                    categories: [
                        '经销商1',
                        '经销商2',
                        '经销商3',
                        '经销商4',
                        '经销商5',
                        '经销商6',
                        '经销商7',
                        '经销商8',
                        '经销商9',
                        '经销商10',
                        '经销商11',
                        '经销商12',
                        '经销商13',
                        '经销商14',
                        '经销商15'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: '数量 (件)'
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: [{
                    name: '库存',
                    data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4, 190.1, 97.6, 84.4]
                }, {
                    name: '发货',
                    data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3, 63.5, 86.6, 52.3]
                }],
                credits: {
                    enabled: false // 禁用版权信息
                }
            });
        });
    </script>
</body>
</html>
