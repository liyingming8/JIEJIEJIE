<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_WXSMreport.aspx.cs" Inherits="TB_wuliu_WXSMreport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html>
    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <link href="../include/MasterPage.css" rel="stylesheet" />
        <script src="http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js"></script>

        <script src="js/highcharts.js"></script>

        <script src="js/exporting.js"></script>

        <script src="js/grid.js"></script>
    </head>
    <body>
        <form runat="server">
            <div class="div_WholePage">
            <div  class="div_Nav"  style="position: relative; z-index: 100">
                <asp:ScriptManager ID="ScriptManager1" EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server"></asp:ScriptManager>
                <table  class="table_Nav" style="width:30%" >
                    <tr>
                        <td class="td_Nav">起始日期: </td>
                        <td class="td_Nav">
                            <asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM" runat="server"></cc1:CalendarExtender>
                        </td>
                         <td class="td_Nav">至</td>
                         <td class="td1">
                            <asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM" runat="server"></cc1:CalendarExtender>
                        </td> 
                         <td class="td1">
                            <asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="inputbutton" OnClick="BtnSearch0_Click" /></td>
                    </tr>
                </table> 
            </div> 
            <div id="container" style="width: 900px; height: 400px; margin: 0 auto; z-index: 1; position: relative; padding-top:3%"></div>
                </div>
        </form>

        <script>

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

            $(function() {

                //得到微信扫描量，Y坐标
                var wcs = "<%= wxcs %>".split(","),
                    wxsm = [];
                for (var i in wcs) {
                    wxsm.push(Number(wcs[i]));
                }
                //其他扫描量，Y坐标

                var qcs = "<%= qtcs %>".split(","),
                    qtsm = [];
                for (var i in qcs) {
                    qtsm.push(Number(qcs[i]));
                }

                //得到X坐标月份
                var m = "<%= mh %>".split(",");

                $('#container').highcharts({
                    chart: {
                        backgroundColor: {
                            linearGradient: [0, 0, 500, 500],
                            stops: [
                                [0, 'rgb(255, 255, 255)'],
                                [1, 'rgb(192, 255, 192)']
                            ]
                        },
                        type: 'line',


                    },
                    title: {
                        text: '查询统计',
                        x: -20

                    },
                    subtitle: {
                        text: '微信扫描量',
                        x: -20
                    },
                    xAxis: {
                        categories: m
                    },
                    yAxis: {
                        title: {
                            text: '次   数'
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
                        backgroundColor: '#FCFFC5'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [
                        {
                            name: '微信',
                            data: wxsm
                        }, {
                            name: '其他',
                            data: qtsm

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