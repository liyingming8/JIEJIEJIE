<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_SWMWXSMreport.aspx.cs" Inherits="TB_SWMWXSMreport" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title></title>
    <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
    <link href="../include/easyui.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <div class="div_WholePage">
            <div class="topdiv" style="position: relative; z-index: 100">
                <div class="topitem">年份</div>
                <div class="topitem">
                    <asp:DropDownList runat="server" ID="ddl_year"/>
                    </div> 
                <div class="topitem">
                     </div>
                <div class="topitem">
                    <asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />
                </div>
                <div class="topitem">
                    <input id="BtDC" runat="server" type="button" class="btn btn-default btnyd" value="导出EXCEL" onserverclick="BtDC_ServerClick" />
                </div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server"></asp:ScriptManager>
            </div>
            <div id="container" style="width: 100%; margin: 20px auto; z-index: 1; position: relative;"></div>
        </div>
    </form>
    <script src="../js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="../js/tuxingshow/highcharts.js"></script>
    <script type="text/javascript" src="../js/tuxingshow/highcharts-zh_CN.js"></script>
    <script type="text/javascript" src="../js/tuxingshow/modules/exporting.js"></script>
    <script type="text/javascript">
        Highcharts.setOptions({
            lang: {
                contextButtonTitle: "图表导出菜单",
                decimalPoint: ".",
                downloadJPEG: "下载JPEG图片",
                downloadPDF: "下载PDF文件",
                downloadPNG: "下载PNG文件",
                downloadSVG: "下载SVG文件",
                drillUpText: "返回 {series.name}",
                loading: "加载中",
                noData: "没有数据",
                printChart: "打印图表",
                resetZoom: "恢复缩放",
                resetZoomTitle: "恢复图表",
            }
        });

        $(function () {
            //得到微信扫描量，Y坐标
            var wcs = "<%= wxcs %>".split(","),
                    wxsm = [];
            for (var i in wcs) {
                wxsm.push(Number(wcs[i]));
            }
            //其他扫描量，Y坐标 
            //得到X坐标月份
            var m = "<%= mh %>".split(",");

                $('#container').highcharts({
                    chart: {
                        type: 'line',
                    },
                    title: {
                        text: '扫码量按月统计',
                        x: -20
                    },
                    xAxis: {
                        categories: m
                    },
                    yAxis: {
                        title: {
                            text: '次数',
                            x: 0
                        },
                        plotLines: [
                            {
                                value: 0,
                                width: 1,
                                color: '#808080'
                            }
                        ]
                    },
                    tooltip: {
                        valueSuffix: '次',
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'center',
                        verticalAlign: 'bottom',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: '扫码量',
                            data: wxsm
                        }
                    ],
                    plotOptions: {
                        line: {
                            dataLabels: {
                                enabled: true,
                                format: '{y} \t 次',
                            }
                        }
                    },
                    credits: {
                        enabled: false // 默认值，如果想去掉版权信息，设置为false即可 
                    }
                });
        });
    </script>
</body>
</html>
