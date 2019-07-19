<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TB_WXXBtongji.aspx.cs" Inherits="TB_WXXBtongji" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html>
    <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
        <title></title>
         
        <link href="../include/MasterPage.css" rel="stylesheet" type="text/css" />
        <link href="../include/easyui.css" rel="stylesheet" />     
    </head>
    <body>
        <form id="Form1" runat="server">
            <div class="div_WholePage">
                <div class="container topdiv" style="position: relative; z-index: 100">
                    <div class="topitem"><span>日期</span></div> 
                    <div class="topitem"><asp:TextBox ID="txtStartDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="yyyy-MM" runat="server"></cc1:CalendarExtender></div> 
                    <div class="topitem"><span>至</span></div> 
                    <div class="topitem"><asp:TextBox ID="txtEndDate" CssClass="input5" runat="server"></asp:TextBox><cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="yyyy-MM" runat="server"></cc1:CalendarExtender></div> 
                    <div class="topitem"><asp:Button ID="BtnSearch0" runat="server" Text="开始汇总" CssClass="btn btn-warning btnyd" OnClick="BtnSearch0_Click" />  </div> 
                    <div class="topitem"><input id="BtDC" runat="server" type="button" class="btn btn-default btnyd" value="导出EXCEL" onserverclick="BtDC_ServerClick" /></div>
                <asp:ScriptManager ID="ScriptManager1" EnableScriptLocalization="true" EnableScriptGlobalization="true" runat="server"></asp:ScriptManager> 
            </div> 
            <div id="container" style="width: 90%; height: 500px; margin: 20px auto; z-index: -1; position: relative; "></div> 
        </div>
        </form> 
        <script src="js/jquery-2.1.1.min.js"></script>
        <script src="js/HG/js/highcharts.js"></script> 
        <script src="js/HG/js/modules/exporting.js"></script> 
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

                //得到男扫描量，Y坐标
                var wcs = "<%= man %>".split(","),
                    msm = [];
                for (var i in wcs) {
                    msm.push(Number(wcs[i]));
                }
                //女扫描量，Y坐标

                var qcs = "<%= women %>".split(","),
                    wmsm = [];
                for (var i in qcs) {
                    wmsm.push(Number(qcs[i]));
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
                        type: 'column',
                    },

                    title: {
                        text: '扫码性别维度统计',
                        x: -20
                    },
                    subtitle: {
                        text: '扫码量',
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
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y} 次</b></td></tr>',
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true,
                        style: {
                            fontSize: '15px'
                        },
                        backgroundColor: '#FCFFC5'
                    },
                    legend: {
                        layout: 'vertical',

                        borderWidth: 0
                    },
                    series: [
                        {
                            name: '男性',
                            data: msm
                        }, {
                            name: '女性',
                            data: wmsm

                        }
                    ],

                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true,
                                format: '{point.y} 次'
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